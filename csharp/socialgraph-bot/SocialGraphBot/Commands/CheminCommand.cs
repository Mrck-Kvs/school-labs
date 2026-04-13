using Discord;
using Discord.Interactions;
using SocialGraphBot.Services;

namespace SocialGraphBot.Commands;

/// <summary>
/// Commande /chemin - Trouve le plus court chemin entre deux utilisateurs
/// UC5: Affiche la chaîne de connexions avec poids
/// UC6: Affiche un message si aucun chemin n'existe
/// </summary>
public class CheminCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly Services.InteractionService _interactionService;

    public CheminCommand(Services.InteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    [SlashCommand("chemin", "Trouve le chemin social entre toi et un autre utilisateur")]
    public async Task CheminAsync(
        [Summary("utilisateur", "L'utilisateur vers qui chercher un chemin")]
        IUser targetUser)
    {
        await DeferAsync();

        // Vérifier qu'on ne cherche pas un chemin vers soi-même
        if (targetUser.Id == Context.User.Id)
        {
            var selfEmbed = new EmbedBuilder()
                .WithTitle("🔗 Chemin vers toi-même")
                .WithDescription("Tu es déjà là ! 😄")
                .WithColor(Color.Orange)
                .WithFooter("🤖 SocialGraph Bot")
                .WithCurrentTimestamp()
                .Build();

            await FollowupAsync(embed: selfEmbed);
            return;
        }

        var result = await _interactionService.FindShortestPathAsync(
            Context.User.Id,
            targetUser.Id);

        var embed = new EmbedBuilder()
            .WithTitle($"🔗 Chemin vers {targetUser.Username}")
            .WithColor(result.Found ? Color.Green : Color.Red);

        if (!result.Found)
        {
            // UC6: Aucune relation trouvée
            embed.WithDescription("❌ **Aucune relation trouvée**\n\n" +
                                  "Tu n'as aucune connexion directe ou indirecte avec cet utilisateur.\n" +
                                  "Mentionne-le dans un channel surveillé pour créer un lien !");
        }
        else
        {
            // UC5: Afficher le chemin
            var pathVisualization = BuildPathVisualization(result);
            embed.WithDescription(pathVisualization);

            embed.AddField("📏 Distance", $"{result.Distance} saut(s)", inline: true);
            embed.AddField("⚖️ Poids total", result.TotalWeight.ToString(), inline: true);
        }

        embed.WithFooter("🤖 SocialGraph Bot")
            .WithCurrentTimestamp();

        await FollowupAsync(embed: embed.Build());
    }

    /// <summary>
    /// Construit une visualisation texte du chemin
    /// Format: Toi ──(47)──▶ Bob ──(23)──▶ Charlie
    /// </summary>
    private string BuildPathVisualization(PathResult result)
    {
        if (result.Steps.Count == 0)
            return "Aucun chemin";

        var parts = new List<string>();

        for (int i = 0; i < result.Steps.Count; i++)
        {
            var step = result.Steps[i];
            var username = i == 0 ? "**Toi**" : $"**{step.Vertex?.Username ?? "?"}**";

            parts.Add(username);

            // Ajouter la flèche avec le poids si ce n'est pas le dernier élément
            if (i < result.Steps.Count - 1 && result.Steps[i + 1].Edge != null)
            {
                var weight = result.Steps[i + 1].Edge!.Weight;
                parts.Add($" ──({weight})──▶ ");
            }
        }

        return string.Concat(parts);
    }
}
