using SocialGraphBot.Models;
using SocialGraphBot.Repositories;

namespace SocialGraphBot.Services;

/// <summary>
/// Service gérant la configuration des channels surveillés par guild
/// </summary>
public class ConfigService
{
    private readonly ConfigRepository _configRepo;

    public ConfigService(ConfigRepository configRepo)
    {
        _configRepo = configRepo;
    }

    /// <summary>
    /// Ajoute un channel à surveiller
    /// </summary>
    public async Task<ConfigResult> AddWatchedChannelAsync(ulong guildId, string guildName, ulong channelId)
    {
        var added = await _configRepo.AddWatchedChannelAsync(
            guildId.ToString(),
            guildName,
            channelId.ToString());

        return new ConfigResult
        {
            Success = added,
            Message = added
                ? $"Le channel sera maintenant surveillé."
                : $"Ce channel est déjà surveillé."
        };
    }

    /// <summary>
    /// Retire un channel de la surveillance
    /// </summary>
    public async Task<ConfigResult> RemoveWatchedChannelAsync(ulong guildId, ulong channelId)
    {
        var removed = await _configRepo.RemoveWatchedChannelAsync(
            guildId.ToString(),
            channelId.ToString());

        return new ConfigResult
        {
            Success = removed,
            Message = removed
                ? $"Le channel ne sera plus surveillé."
                : $"Ce channel n'était pas surveillé."
        };
    }

    /// <summary>
    /// Récupère la liste des channels surveillés
    /// </summary>
    public async Task<List<string>> GetWatchedChannelsAsync(ulong guildId)
    {
        return await _configRepo.GetWatchedChannelsAsync(guildId.ToString());
    }

    /// <summary>
    /// Vérifie si un channel est surveillé
    /// </summary>
    public async Task<bool> IsChannelWatchedAsync(ulong guildId, ulong channelId)
    {
        return await _configRepo.IsChannelWatchedAsync(
            guildId.ToString(),
            channelId.ToString());
    }

    /// <summary>
    /// Récupère la configuration complète d'une guild
    /// </summary>
    public async Task<GuildConfig?> GetGuildConfigAsync(ulong guildId)
    {
        return await _configRepo.GetByKeyAsync(guildId.ToString());
    }
}

/// <summary>
/// Résultat d'une opération de configuration
/// </summary>
public class ConfigResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
