using Discord;
using SocialGraphBot.Models;
using SocialGraphBot.Repositories;

namespace SocialGraphBot.Services;

/// <summary>
/// Service gérant la logique métier liée aux utilisateurs
/// </summary>
public class UserService
{
    private readonly UserRepository _userRepo;
    private readonly InteractionRepository _interactionRepo;

    public UserService(UserRepository userRepo, InteractionRepository interactionRepo)
    {
        _userRepo = userRepo;
        _interactionRepo = interactionRepo;
    }

    /// <summary>
    /// Récupère ou crée un utilisateur
    /// </summary>
    public async Task<User> GetOrCreateUserAsync(ulong discordId, string username, DateTime? joinedAt = null)
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
        else if (user.Username != username)
        {
            // Met à jour le username s'il a changé
            user.Username = username;
            await _userRepo.UpsertAsync(user);
        }

        return user;
    }

    /// <summary>
    /// Récupère le profil social complet d'un utilisateur
    /// </summary>
    public async Task<UserProfile?> GetUserProfileAsync(ulong discordId)
    {
        var key = discordId.ToString();
        var user = await _userRepo.GetByKeyAsync(key);

        if (user == null)
            return null;

        var connections = await _interactionRepo.CountConnectionsAsync(key);
        var topConnections = await _interactionRepo.GetTopConnectionsAsync(key, 5);

        return new UserProfile
        {
            User = user,
            ConnectionCount = connections,
            TopConnections = topConnections
        };
    }

    /// <summary>
    /// Récupère le classement des utilisateurs par réputation
    /// </summary>
    public async Task<List<User>> GetLeaderboardAsync(int limit = 10)
    {
        return await _userRepo.GetTopByReputationAsync(limit);
    }

    /// <summary>
    /// Récupère toutes les connexions d'un utilisateur
    /// </summary>
    public async Task<List<ConnectionInfo>> GetUserRelationsAsync(ulong discordId)
    {
        return await _interactionRepo.GetUserConnectionsAsync(discordId.ToString());
    }
}

/// <summary>
/// DTO pour le profil utilisateur complet
/// </summary>
public class UserProfile
{
    public User User { get; set; } = new();
    public int ConnectionCount { get; set; }
    public List<ConnectionInfo> TopConnections { get; set; } = new();
}
