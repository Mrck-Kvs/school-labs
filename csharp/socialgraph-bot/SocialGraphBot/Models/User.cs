using System.Text.Json.Serialization;

namespace SocialGraphBot.Models;

/// <summary>
/// Représente un utilisateur Discord stocké dans ArangoDB
/// Collection: users (Document Collection)
/// </summary>
public class User
{
    /// <summary>
    /// Clé ArangoDB - correspond à l'ID Discord
    /// </summary>
    [JsonPropertyName("_key")]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// ID interne ArangoDB (lecture seule)
    /// </summary>
    [JsonPropertyName("_id")]
    public string? Id { get; set; }

    /// <summary>
    /// Nom d'utilisateur Discord
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Date d'arrivée sur le serveur Discord
    /// </summary>
    [JsonPropertyName("joinedAt")]
    public DateTime? JoinedAt { get; set; }

    /// <summary>
    /// Nombre total de mentions envoyées par cet utilisateur
    /// </summary>
    [JsonPropertyName("totalMentionsSent")]
    public int TotalMentionsSent { get; set; } = 0;

    /// <summary>
    /// Nombre total de mentions reçues = Réputation
    /// </summary>
    [JsonPropertyName("totalMentionsReceived")]
    public int TotalMentionsReceived { get; set; } = 0;
}
