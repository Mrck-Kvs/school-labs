using SocialGraphBot.Models;
using System.Net;

namespace SocialGraphBot.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly ArangoDbContext _context;

    public UserRepository(ArangoDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByKeyAsync(string key)
    {
        var aql = "FOR u IN users FILTER u._key == @key RETURN u";
        var result = await _context.ExecuteAqlAsync<User>(aql,
            new Dictionary<string, object> { { "key", key } });
        return result.FirstOrDefault();
    }

    public async Task<User> UpsertAsync(User user)
    {
        var aql = @"
            UPSERT { _key: @key }
            INSERT { 
                _key: @key, 
                username: @username, 
                joinedAt: @joinedAt,
                totalMentionsSent: @sent,
                totalMentionsReceived: @received
            }
            UPDATE { 
                username: @username
            }
            IN users
            RETURN NEW";

        var result = await _context.ExecuteAqlAsync<User>(aql, new Dictionary<string, object>
        {
            { "key", user.Key },
            { "username", user.Username },
            { "joinedAt", user.JoinedAt?.ToString("o") ?? null! },
            { "sent", user.TotalMentionsSent },
            { "received", user.TotalMentionsReceived }
        });

        return result.First();
    }

    public async Task<bool> DeleteAsync(string key)
    {
        var aql = "REMOVE { _key: @key } IN users";
        try
        {
            await _context.ExecuteAqlAsync<object>(aql,
                new Dictionary<string, object> { { "key", key } });
            return true;
        }
        catch { return false; }
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var aql = "FOR u IN users RETURN u";
        return await _context.ExecuteAqlAsync<User>(aql);
    }

    public async Task IncrementMentionsSentAsync(string key)
    {
        var aql = @"
            UPDATE { _key: @key } 
            WITH { totalMentionsSent: OLD.totalMentionsSent + 1 } 
            IN users";

        await _context.ExecuteAqlAsync<object>(aql,
            new Dictionary<string, object> { { "key", key } });
    }

    public async Task IncrementMentionsReceivedAsync(string key)
    {
        var aql = @"
            UPDATE { _key: @key } 
            WITH { totalMentionsReceived: OLD.totalMentionsReceived + 1 } 
            IN users";

        await _context.ExecuteAqlAsync<object>(aql,
            new Dictionary<string, object> { { "key", key } });
    }

    public async Task<List<User>> GetTopByReputationAsync(int limit = 10)
    {
        var aql = @"
            FOR u IN users
            SORT u.totalMentionsReceived DESC
            LIMIT @limit
            RETURN u";

        return await _context.ExecuteAqlAsync<User>(aql,
            new Dictionary<string, object> { { "limit", limit } });
    }

    public async Task<int> CountAsync()
    {
        var aql = "RETURN LENGTH(users)";
        var result = await _context.ExecuteAqlAsync<int>(aql);
        return result.FirstOrDefault();
    }
}