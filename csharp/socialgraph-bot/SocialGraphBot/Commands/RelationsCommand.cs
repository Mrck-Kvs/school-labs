using Discord;
using Discord.Interactions;
using SocialGraphBot.Services;

namespace SocialGraphBot.Commands;

/// <summary>
/// Commande /relations - Liste toutes les connexions de l'utilisateur
/// UC4: Affiche toutes les connexions avec poids et niveau
/// </summary>
public class RelationsCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly UserService _userService;

    public RelationsCommand(UserService userService)
    {
        _userService = userService;
    }

    [SlashCommand("relations", "Affiche la liste de toutes tes connexions")]
    public async Task RelationsAsync()
    {
        await DeferAsync();

        var connections = await _userService.GetUserRelationsAsync(Context.User.Id);

        var embed = new EmbedBuilder()
            .WithTitle($"🔗 Connexions de {Context.User.Username}")
            .WithColor(Color.Teal)
            .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());

        if (!connections.Any())
        {
            embed.WithDescription("Tu n'as aucune connexion pour le moment.\n" +
                                  "Mentionne des utilisateurs dans les channels surveillés pour créer des liens !");
        }
        else
        {
            embed.WithDescription($"**{connections.Count} connexion(s) trouvée(s)**\n\u200b");

            // Grouper par niveau de relation pour un affichage plus clair
            var amiProche = connections.Where(c => c.Level == "Ami proche").ToList();
            var ami = connections.Where(c => c.Level == "Ami").ToList();
            var connaissance = connections.Where(c => c.Level == "Connaissance").ToList();
            var inconnu = connections.Where(c => c.Level == "Inconnu").ToList();

            if (amiProche.Any())
            {
                var text = string.Join("\n", amiProche.Select(c =>
                    $"• {c.User.Username} - **{c.Weight}** mentions"));
                embed.AddField($"💜 Amis proches ({amiProche.Count})", text, inline: false);
            }

            if (ami.Any())
            {
                var text = string.Join("\n", ami.Select(c =>
                    $"• {c.User.Username} - **{c.Weight}** mentions"));
                embed.AddField($"💙 Amis ({ami.Count})", text, inline: false);
            }

            if (connaissance.Any())
            {
                var text = string.Join("\n", connaissance.Select(c =>
                    $"• {c.User.Username} - **{c.Weight}** mentions"));
                embed.AddField($"💚 Connaissances ({connaissance.Count})", text, inline: false);
            }

            if (inconnu.Any())
            {
                var text = string.Join("\n", inconnu.Take(10).Select(c =>
                    $"• {c.User.Username} - **{c.Weight}** mentions"));
                if (inconnu.Count > 10)
                    text += $"\n... et {inconnu.Count - 10} autres";
                embed.AddField($"⬜ Inconnus ({inconnu.Count})", text, inline: false);
            }
        }

        embed.WithFooter("🤖 SocialGraph Bot • Niveaux: Inconnu (0-9) | Connaissance (10-49) | Ami (50-99) | Ami proche (100+)")
            .WithCurrentTimestamp();

        await FollowupAsync(embed: embed.Build());
    }
}
