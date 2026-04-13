using Discord;
using Discord.Interactions;
using SocialGraphBot.Services;

namespace SocialGraphBot.Commands;

/// <summary>
/// Commande /stats - Affiche les statistiques globales du serveur
/// UC11: Statistiques pour les admins
/// </summary>
[DefaultMemberPermissions(GuildPermission.Administrator)]
public class StatsCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly Services.InteractionService _interactionService;
    private readonly ConfigService _configService;

    public StatsCommand(Services.InteractionService interactionService, ConfigService configService)
    {
        _interactionService = interactionService;
        _configService = configService;
    }

    [SlashCommand("stats", "Affiche les statistiques globales du serveur")]
    public async Task StatsAsync()
    {
        await DeferAsync(ephemeral: true);

        var stats = await _interactionService.GetGraphStatsAsync();
        var watchedChannels = await _configService.GetWatchedChannelsAsync(Context.Guild.Id);

        var embed = new EmbedBuilder()
            .WithTitle("📊 Statistiques du Serveur")
            .WithColor(Color.Purple)
            .WithThumbnailUrl(Context.Guild.IconUrl);

        embed.AddField("👥 Users trackés", stats.TotalUsers.ToString("N0"), inline: true);
        embed.AddField("💬 Total interactions", stats.TotalInteractions.ToString("N0"), inline: true);
        embed.AddField("🔗 Connexions uniques", stats.TotalEdges.ToString("N0"), inline: true);
        embed.AddField("📺 Channels surveillés", watchedChannels.Count.ToString(), inline: true);

        if (stats.MostConnectedUser != null)
        {
            embed.AddField("👑 Plus connecté",
                $"{stats.MostConnectedUser.User.Username} ({stats.MostConnectedUser.Connections} connexions)",
                inline: false);
        }
        else
        {
            embed.AddField("👑 Plus connecté", "Aucune donnée", inline: false);
        }

        // Calculs supplémentaires
        if (stats.TotalEdges > 0 && stats.TotalUsers > 0)
        {
            var avgConnectionsPerUser = (double)stats.TotalEdges / stats.TotalUsers;
            var avgInteractionsPerEdge = stats.TotalEdges > 0
                ? (double)stats.TotalInteractions / stats.TotalEdges
                : 0;

            embed.AddField("📈 Moyennes",
                $"**{avgConnectionsPerUser:F1}** connexions/utilisateur\n" +
                $"**{avgInteractionsPerEdge:F1}** mentions/connexion",
                inline: false);
        }

        embed.WithFooter("🤖 SocialGraph Bot")
            .WithCurrentTimestamp();

        await FollowupAsync(embed: embed.Build(), ephemeral: true);
    }
}
