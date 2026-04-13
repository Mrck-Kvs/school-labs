using Microsoft.VisualBasic;
using SocialGraphBot.Models;

namespace SocialGraphBot.Repositories;

/// <summary>
/// Repository pour la collection interactions (Edge Collection)
/// Gère les relations pondérées entre utilisateurs et les requêtes de graphe
/// </summary>
public class InteractionRepository
{
    private readonly ArangoDbContext _context;

    public InteractionRepository(ArangoDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Récupère une interaction entre deux users
    /// </summary>
    public async Task<Models.Interaction?> GetInteractionAsync(string fromUserId, string toUserId)
    {
        var aql = @"
            FOR e IN interactions
            FILTER e._from == @from AND e._to == @to
            RETURN e";

        var result = await _context.ExecuteAqlAsync<Models.Interaction>(aql, new Dictionary<string, object>
        {
            { "from", $"users/{fromUserId}" },
            { "to", $"users/{toUserId}" }
        });

        return result.FirstOrDefault();
    }

    /// <summary>
    /// Crée ou met à jour une interaction (upsert)
    /// Incrémente le poids si l'interaction existe déjà
    /// </summary>
    public async Task<Models.Interaction> UpsertInteractionAsync(string fromUserId, string toUserId)
    {
        var aql = @"
            UPSERT { _from: @from, _to: @to }
            INSERT { 
                _from: @from, 
                _to: @to, 
                weight: 1, 
                firstInteraction: DATE_ISO8601(DATE_NOW()),
                lastInteraction: DATE_ISO8601(DATE_NOW())
            }
            UPDATE { 
                weight: OLD.weight + 1,
                lastInteraction: DATE_ISO8601(DATE_NOW())
            }
            IN interactions
            RETURN NEW";

        var result = await _context.ExecuteAqlAsync<Models.Interaction>(aql, new Dictionary<string, object>
        {
            { "from", $"users/{fromUserId}" },
            { "to", $"users/{toUserId}" }
        });

        return result.First();
    }

    /// <summary>
    /// Récupère toutes les connexions sortantes d'un utilisateur avec le niveau de relation
    /// </summary>
    public async Task<List<ConnectionInfo>> GetUserConnectionsAsync(string userId)
    {
        var aql = @"
            FOR v, e IN 1..1 OUTBOUND @userId interactions
            LET level = (
                e.weight >= 100 ? 'Ami proche' :
                e.weight >= 50 ? 'Ami' :
                e.weight >= 10 ? 'Connaissance' : 'Inconnu'
            )
            SORT e.weight DESC
            RETURN { 
                user: v, 
                weight: e.weight, 
                level: level,
                firstInteraction: e.firstInteraction,
                lastInteraction: e.lastInteraction
            }";

        return await _context.ExecuteAqlAsync<ConnectionInfo>(aql, new Dictionary<string, object>
        {
            { "userId", $"users/{userId}" }
        });
    }

    /// <summary>
    /// Calcule le plus court chemin entre deux utilisateurs
    /// Utilise SHORTEST_PATH de ArangoDB pour la traversée de graphe
    /// </summary>
    public async Task<List<PathStep>> GetShortestPathAsync(string fromUserId, string toUserId)
    {
        var aql = @"
            FOR v, e IN OUTBOUND SHORTEST_PATH 
                @from TO @to 
                interactions
            RETURN { vertex: v, edge: e }";

        return await _context.ExecuteAqlAsync<PathStep>(aql, new Dictionary<string, object>
        {
            { "from", $"users/{fromUserId}" },
            { "to", $"users/{toUserId}" }
        });
    }

    /// <summary>
    /// Récupère le top 5 des relations d'un utilisateur
    /// </summary>
    public async Task<List<ConnectionInfo>> GetTopConnectionsAsync(string userId, int limit = 5)
    {
        var aql = @"
            FOR v, e IN 1..1 OUTBOUND @userId interactions
            LET level = (
                e.weight >= 100 ? 'Ami proche' :
                e.weight >= 50 ? 'Ami' :
                e.weight >= 10 ? 'Connaissance' : 'Inconnu'
            )
            SORT e.weight DESC
            LIMIT @limit
            RETURN { 
                user: v, 
                weight: e.weight, 
                level: level 
            }";

        return await _context.ExecuteAqlAsync<ConnectionInfo>(aql, new Dictionary<string, object>
        {
            { "userId", $"users/{userId}" },
            { "limit", limit }
        });
    }

    /// <summary>
    /// Compte le nombre de connexions d'un utilisateur
    /// </summary>
    public async Task<int> CountConnectionsAsync(string userId)
    {
        var aql = @"
            RETURN LENGTH(
                FOR v IN 1..1 OUTBOUND @userId interactions RETURN 1
            )";

        var result = await _context.ExecuteAqlAsync<int>(aql, new Dictionary<string, object>
        {
            { "userId", $"users/{userId}" }
        });

        return result.FirstOrDefault();
    }

    /// <summary>
    /// Compte le nombre total d'edges
    /// </summary>
    public async Task<int> CountEdgesAsync()
    {
        var aql = "RETURN LENGTH(interactions)";
        var result = await _context.ExecuteAqlAsync<int>(aql);
        return result.FirstOrDefault();
    }

    /// <summary>
    /// Calcule la somme totale des interactions (poids)
    /// </summary>
    public async Task<int> GetTotalInteractionsAsync()
    {
        var aql = "RETURN SUM(FOR i IN interactions RETURN i.weight)";
        var result = await _context.ExecuteAqlAsync<int?>(aql);
        return result.FirstOrDefault() ?? 0;
    }

    /// <summary>
    /// Trouve l'utilisateur le plus connecté
    /// </summary>
    public async Task<MostConnectedUser?> GetMostConnectedUserAsync()
    {
        var aql = @"
            FOR u IN users
            LET connections = LENGTH(FOR v IN 1..1 ANY u interactions RETURN 1)
            SORT connections DESC
            LIMIT 1
            RETURN { user: u, connections: connections }";

        var result = await _context.ExecuteAqlAsync<MostConnectedUser>(aql);
        return result.FirstOrDefault();
    }
}

/// <summary>
/// DTO pour les informations de connexion
/// </summary>
public class ConnectionInfo
{
    public User User { get; set; } = new();
    public int Weight { get; set; }
    public string Level { get; set; } = string.Empty;
    public DateTime? FirstInteraction { get; set; }
    public DateTime? LastInteraction { get; set; }
}

/// <summary>
/// DTO pour les étapes du chemin
/// </summary>
public class PathStep
{
    public User? Vertex { get; set; }
    public Models.Interaction? Edge { get; set; }
}

/// <summary>
/// DTO pour l'utilisateur le plus connecté
/// </summary>
public class MostConnectedUser
{
    public User User { get; set; } = new();
    public int Connections { get; set; }
}
