using Discord;
using Discord.Interactions;
using SocialGraphBot.Services;

namespace SocialGraphBot.Commands;

/// <summary>
/// Commande /top - Affiche le classement des utilisateurs par réputation
/// UC7: Top 10 des réputations
/// </summary>
public class TopCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly UserService _userService;

    public TopCommand(UserService userService)
    {
        _userService = userService;
    }

    [SlashCommand("top", "Affiche le classement des 10 meilleures réputations")]
    public async Task TopAsync()
    {
        await DeferAsync();

        var leaderboard = await _userService.GetLeaderboardAsync(10);

        var embed = new EmbedBuilder()
            .WithTitle("🏆 Top 10 - Réputation")
            .WithColor(Color.Gold);

        if (!leaderboard.Any())
        {
            embed.WithDescription("Aucun utilisateur n'a encore de réputation.\n" +
                                  "Commencez à vous mentionner dans les channels surveillés !");
        }
        else
        {
            var rankingText = new List<string>();

            for (int i = 0; i < leaderboard.Count; i++)
            {
                var user = leaderboard[i];
                var medal = i switch
                {
                    0 => "👑",
                    1 => "🥈",
                    2 => "🥉",
                    _ => $"{i + 1}."
                };

                rankingText.Add($"{medal} **{user.Username}** - {user.TotalMentionsReceived} points");
            }

            embed.WithDescription(string.Join("\n", rankingText));
        }

        embed.WithFooter("🤖 SocialGraph Bot • La réputation = nombre de mentions reçues")
            .WithCurrentTimestamp();

        await FollowupAsync(embed: embed.Build());
    }
}
