using Discord;
using Discord.Interactions;
using SocialGraphBot.Services;

namespace SocialGraphBot.Commands;

/// <summary>
/// Groupe de commandes /config pour la gestion des channels surveillés
/// UC8, UC9, UC10: add, remove, list
/// Réservé aux administrateurs
/// </summary>
[Group("config", "Configuration des channels surveillés")]
[DefaultMemberPermissions(GuildPermission.Administrator)]
public class ConfigCommand : InteractionModuleBase<SocketInteractionContext>
{
    private readonly ConfigService _configService;

    public ConfigCommand(ConfigService configService)
    {
        _configService = configService;
    }

    /// <summary>
    /// UC8: Ajoute un channel à surveiller
    /// </summary>
    [SlashCommand("add", "Ajoute un channel à surveiller")]
    public async Task AddAsync(
        [Summary("channel", "Le channel à surveiller")]
        ITextChannel channel)
    {
        await DeferAsync(ephemeral: true);

        var result = await _configService.AddWatchedChannelAsync(
            Context.Guild.Id,
            Context.Guild.Name,
            channel.Id);

        var embed = new EmbedBuilder()
            .WithTitle("⚙️ Configuration")
            .WithDescription(result.Success
                ? $"✅ {channel.Mention} est maintenant surveillé."
                : $"⚠️ {channel.Mention} était déjà surveillé.")
            .WithColor(result.Success ? Color.Green : Color.Orange)
            .WithFooter("🤖 SocialGraph Bot")
            .WithCurrentTimestamp()
            .Build();

        await FollowupAsync(embed: embed, ephemeral: true);
    }

    /// <summary>
    /// UC9: Retire un channel de la surveillance
    /// </summary>
    [SlashCommand("remove", "Retire un channel de la surveillance")]
    public async Task RemoveAsync(
        [Summary("channel", "Le channel à retirer")]
        ITextChannel channel)
    {
        await DeferAsync(ephemeral: true);

        var result = await _configService.RemoveWatchedChannelAsync(
            Context.Guild.Id,
            channel.Id);

        var embed = new EmbedBuilder()
            .WithTitle("⚙️ Configuration")
            .WithDescription(result.Success
                ? $"✅ {channel.Mention} n'est plus surveillé."
                : $"⚠️ {channel.Mention} n'était pas surveillé.")
            .WithColor(result.Success ? Color.Green : Color.Orange)
            .WithFooter("🤖 SocialGraph Bot")
            .WithCurrentTimestamp()
            .Build();

        await FollowupAsync(embed: embed, ephemeral: true);
    }

    /// <summary>
    /// UC10: Liste les channels surveillés
    /// </summary>
    [SlashCommand("list", "Liste les channels surveillés")]
    public async Task ListAsync()
    {
        await DeferAsync(ephemeral: true);

        var watchedChannels = await _configService.GetWatchedChannelsAsync(Context.Guild.Id);

        var embed = new EmbedBuilder()
            .WithTitle("⚙️ Channels surveillés")
            .WithColor(Color.Blue);

        if (!watchedChannels.Any())
        {
            embed.WithDescription("Aucun channel n'est surveillé.\n" +
                                  "Utilisez `/config add #channel` pour en ajouter.");
        }
        else
        {
            var channelMentions = watchedChannels
                .Select(id => $"<#{id}>")
                .ToList();

            embed.WithDescription($"**{watchedChannels.Count} channel(s) surveillé(s):**\n\n" +
                                  string.Join("\n", channelMentions));
        }

        embed.WithFooter("🤖 SocialGraph Bot")
            .WithCurrentTimestamp();

        await FollowupAsync(embed: embed.Build(), ephemeral: true);
    }
}
