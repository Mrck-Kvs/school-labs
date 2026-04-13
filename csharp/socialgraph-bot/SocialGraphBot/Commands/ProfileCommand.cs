using Discord;
using Discord.Interactions;
using SocialGraphBot.Services;

namespace SocialGraphBot.Commands
{
    /// <summary>
    /// Commande /profil - Affiche le profil social d'un utilisateur
    /// UC2: /profil → son propre profil
    /// UC3: /profil @user → profil d'un autre
    /// </summary>
    public class ProfileCommand : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly UserService _userService;

        public ProfileCommand(UserService userService)
        {
            _userService = userService;
        }

        [SlashCommand("profil", "Affiche le profil social d'un utilisateur")]
        public async Task ProfileAsync(
            [Summary("utilisateur", "L'utilisateur dont afficher le profil (optionnel)")]
        IUser? user = null)
        {
            await DeferAsync();

            var targetUser = user ?? Context.User;
            var profile = await _userService.GetUserProfileAsync(targetUser.Id);

            if (profile == null)
            {
                var notFoundEmbed = new EmbedBuilder()
                    .WithTitle($"👤 Profil Social de {targetUser.Username}")
                    .WithDescription("Aucune donnée trouvée pour cet utilisateur.\nIl n'a pas encore participé aux discussions surveillées.")
                    .WithColor(Color.Orange)
                    .WithFooter("🤖 SocialGraph Bot")
                    .WithCurrentTimestamp()
                    .Build();

                await FollowupAsync(embed: notFoundEmbed);
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle($"👤 Profil Social de {profile.User.Username}")
                .WithColor(Color.Blue)
                .WithThumbnailUrl(targetUser.GetAvatarUrl() ?? targetUser.GetDefaultAvatarUrl());

            // Date d'arrivée
            if (profile.User.JoinedAt.HasValue)
            {
                embed.AddField("📅 Membre depuis",
                    profile.User.JoinedAt.Value.ToString("dd MMMM yyyy"),
                    inline: true);
            }

            // Statistiques
            embed.AddField("📊 Statistiques",
                $"**Réputation:** {profile.User.TotalMentionsReceived} points\n" +
                $"**Connexions:** {profile.ConnectionCount} personnes",
                inline: false);

            // Top 5 relations
            if (profile.TopConnections.Any())
            {
                var topRelationsText = string.Join("\n",
                    profile.TopConnections.Select((c, i) =>
                        $"{i + 1}. {c.User.Username} ({c.Weight}) - {c.Level}"));

                embed.AddField("🏆 Top Relations", topRelationsText, inline: false);
            }
            else
            {
                embed.AddField("🏆 Top Relations", "Aucune connexion pour le moment", inline: false);
            }

            embed.WithFooter("🤖 SocialGraph Bot")
                .WithCurrentTimestamp();

            await FollowupAsync(embed: embed.Build());
        }
    }

}

