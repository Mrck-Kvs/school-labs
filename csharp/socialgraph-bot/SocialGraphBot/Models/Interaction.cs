using System.Text.Json.Serialization;

namespace SocialGraphBot.Models;

/// <summary>
/// Représente une relation entre deux utilisateurs (Edge Collection)
/// Collection: interactions (Edge Collection)
/// _from = user qui mentionne, _to = user mentionné
/// </summary>
public class Interaction
{
    /// <summary>
    /// Clé ArangoDB de l'edge
    /// </summary>
    [JsonPropertyName("_key")]
    public string? Key { get; set; }

    /// <summary>
    /// ID interne ArangoDB
    /// </summary>
    [JsonPropertyName("_id")]
    public string? Id { get; set; }

    /// <summary>
    /// Source de l'edge: users/{discordId} - celui qui mentionne
    /// </summary>
    [JsonPropertyName("_from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Destination de l'edge: users/{discordId} - celui qui est mentionné
    /// </summary>
    [JsonPropertyName("_to")]
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// Poids de la relation = nombre de mentions
    /// </summary>
    [JsonPropertyName("weight")]
    public int Weight { get; set; } = 1;

    /// <summary>
    /// Date de la première interaction
    /// </summary>
    [JsonPropertyName("firstInteraction")]
    public DateTime FirstInteraction { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date de la dernière interaction
    /// </summary>
    [JsonPropertyName("lastInteraction")]
    public DateTime LastInteraction { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calcule le niveau de relation basé sur le poids
    /// R5: Inconnu (0-9), Connaissance (10-49), Ami (50-99), Ami proche (100+)
    /// </summary>
    public string GetRelationLevel() => Weight switch
    {
        >= 100 => "Ami proche",
        >= 50 => "Ami",
        >= 10 => "Connaissance",
        _ => "Inconnu"
    };
}
