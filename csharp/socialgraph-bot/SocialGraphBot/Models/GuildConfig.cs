using System.Text.Json.Serialization;

namespace SocialGraphBot.Models;

/// <summary>
/// Configuration d'un serveur Discord
/// Collection: config (Document Collection)
/// Stocke les channels à surveiller par guild
/// </summary>
public class GuildConfig
{
    /// <summary>
    /// Clé ArangoDB - correspond à l'ID du serveur Discord (guild)
    /// </summary>
    [JsonPropertyName("_key")]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// ID interne ArangoDB
    /// </summary>
    [JsonPropertyName("_id")]
    public string? Id { get; set; }

    /// <summary>
    /// Nom du serveur Discord
    /// </summary>
    [JsonPropertyName("guildName")]
    public string GuildName { get; set; } = string.Empty;

    /// <summary>
    /// Liste des IDs des channels surveillés
    /// </summary>
    [JsonPropertyName("watchedChannels")]
    public List<string> WatchedChannels { get; set; } = new();

    /// <summary>
    /// Date de création de la configuration
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Vérifie si un channel est surveillé
    /// </summary>
    public bool IsChannelWatched(ulong channelId) =>
        WatchedChannels.Contains(channelId.ToString());
}
