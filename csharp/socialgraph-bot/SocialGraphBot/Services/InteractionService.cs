using SocialGraphBot.Models;
using SocialGraphBot.Repositories;

namespace SocialGraphBot.Services;

/// <summary>
/// Service gérant les interactions (mentions) entre utilisateurs
/// Implémente la logique de tracking temps réel et de traversée de graphe
/// </summary>
public class InteractionService
{
    private readonly UserRepository _userRepo;
    private readonly InteractionRepository _interactionRepo;
    private readonly ConfigRepository _configRepo;

    public InteractionService(
        UserRepository userRepo,
        InteractionRepository interactionRepo,
        ConfigRepository configRepo)
    {
        _userRepo = userRepo;
        _interactionRepo = interactionRepo;
        _configRepo = configRepo;
    }

    /// <summary>
    /// Traite une mention entre deux utilisateurs
    /// Règles: R1 (seules @user), R3 (plusieurs mentions = plusieurs edges), R4 (pas d'auto-mention)
    /// </summary>
    public async Task<bool> ProcessMentionAsync(
        ulong guildId,
        ulong channelId,
        ulong authorId,
        string authorUsername,
        DateTime? authorJoinedAt,
        ulong mentionedId,
        string mentionedUsername,
        DateTime? mentionedJoinedAt)
    {
        // R4: Ignorer les auto-mentions
        if (authorId == mentionedId)
            return false;

        // R2: Vérifier si le channel est surveillé
        var isWatched = await _configRepo.IsChannelWatchedAsync(
            guildId.ToString(),
            channelId.ToString());

        if (!isWatched)
            return false;

        // Créer/récupérer les users
        var author = await GetOrCreateUserAsync(authorId, authorUsername, authorJoinedAt);
        var mentioned = await GetOrCreateUserAsync(mentionedId, mentionedUsername, mentionedJoinedAt);

        // Créer/incrémenter l'interaction (edge)
        await _interactionRepo.UpsertInteractionAsync(
            authorId.ToString(),
            mentionedId.ToString());

        // Incrémenter les compteurs
        await _userRepo.IncrementMentionsSentAsync(authorId.ToString());
        await _userRepo.IncrementMentionsReceivedAsync(mentionedId.ToString());

        return true;
    }

    /// <summary>
    /// Calcule le plus court chemin entre deux utilisateurs
    /// </summary>
    public async Task<PathResult> FindShortestPathAsync(ulong fromUserId, ulong toUserId)
    {
        var path = await _interactionRepo.GetShortestPathAsync(
            fromUserId.ToString(),
            toUserId.ToString());

        if (path.Count == 0)
        {
            return new PathResult { Found = false };
        }

        var totalWeight = path
            .Where(p => p.Edge != null)
            .Sum(p => p.Edge!.Weight);

        return new PathResult
        {
            Found = true,
            Steps = path,
            Distance = path.Count - 1, // -1 car le premier vertex n'a pas d'edge entrant
            TotalWeight = totalWeight
        };
    }

    /// <summary>
    /// Récupère les statistiques globales du graphe
    /// </summary>
    public async Task<GraphStats> GetGraphStatsAsync()
    {
        var userCount = await _userRepo.CountAsync();
        var edgeCount = await _interactionRepo.CountEdgesAsync();
        var totalInteractions = await _interactionRepo.GetTotalInteractionsAsync();
        var mostConnected = await _interactionRepo.GetMostConnectedUserAsync();

        return new GraphStats
        {
            TotalUsers = userCount,
            TotalEdges = edgeCount,
            TotalInteractions = totalInteractions,
            MostConnectedUser = mostConnected
        };
    }

    private async Task<User> GetOrCreateUserAsync(ulong discordId, string username, DateTime? joinedAt)
    {
        var key = discordId.ToString();
        var user = await _userRepo.GetByKeyAsync(key);

        if (user == null)
        {
            user = new User
            {
                Key = key,
                Username = username,
                JoinedAt = joinedAt,
                TotalMentionsSent = 0,
                TotalMentionsReceived = 0
            };
            await _userRepo.UpsertAsync(user);
        }

        return user;
    }
}

/// <summary>
/// Résultat de la recherche de chemin
/// </summary>
public class PathResult
{
    public bool Found { get; set; }
    public List<PathStep> Steps { get; set; } = new();
    public int Distance { get; set; }
    public int TotalWeight { get; set; }
}

/// <summary>
/// Statistiques globales du graphe social
/// </summary>
public class GraphStats
{
    public int TotalUsers { get; set; }
    public int TotalEdges { get; set; }
    public int TotalInteractions { get; set; }
    public MostConnectedUser? MostConnectedUser { get; set; }
}
