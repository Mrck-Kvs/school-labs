using SocialGraphBot.Models;

namespace SocialGraphBot.Repositories;

public class ConfigRepository : IRepository<GuildConfig>
{
    private readonly ArangoDbContext _context;

    public ConfigRepository(ArangoDbContext context)
    {
        _context = context;
    }

    public async Task<GuildConfig?> GetByKeyAsync(string guildId)
    {
        var aql = "FOR c IN config FILTER c._key == @key RETURN c";
        var result = await _context.ExecuteAqlAsync<GuildConfig>(aql,
            new Dictionary<string, object> { { "key", guildId } });
        return result.FirstOrDefault();
    }

    public async Task<GuildConfig> UpsertAsync(GuildConfig config)
    {
        var aql = @"
            UPSERT { _key: @key }
            INSERT { 
                _key: @key, 
                guildName: @guildName, 
                watchedChannels: @watchedChannels, 
                createdAt: DATE_ISO8601(DATE_NOW()) 
            }
            UPDATE { 
                guildName: @guildName, 
                watchedChannels: @watchedChannels 
            }
            IN config
            RETURN NEW";

        var result = await _context.ExecuteAqlAsync<GuildConfig>(aql, new Dictionary<string, object>
        {
            { "key", config.Key },
            { "guildName", config.GuildName },
            { "watchedChannels", config.WatchedChannels }
        });

        return result.First();
    }

    public async Task<bool> DeleteAsync(string key)
    {
        var aql = "REMOVE { _key: @key } IN config";
        try
        {
            await _context.ExecuteAqlAsync<object>(aql,
                new Dictionary<string, object> { { "key", key } });
            return true;
        }
        catch { return false; }
    }

    public async Task<IEnumerable<GuildConfig>> GetAllAsync()
    {
        var aql = "FOR c IN config RETURN c";
        return await _context.ExecuteAqlAsync<GuildConfig>(aql);
    }

    public async Task<bool> AddWatchedChannelAsync(string guildId, string guildName, string channelId)
    {
        var config = await GetByKeyAsync(guildId);

        if (config == null)
        {
            config = new GuildConfig
            {
                Key = guildId,
                GuildName = guildName,
                WatchedChannels = new List<string> { channelId }
            };
            await UpsertAsync(config);
            return true;
        }

        if (config.WatchedChannels.Contains(channelId))
            return false;

        config.WatchedChannels.Add(channelId);
        await UpsertAsync(config);
        return true;
    }

    public async Task<bool> RemoveWatchedChannelAsync(string guildId, string channelId)
    {
        var config = await GetByKeyAsync(guildId);

        if (config == null || !config.WatchedChannels.Contains(channelId))
            return false;

        config.WatchedChannels.Remove(channelId);
        await UpsertAsync(config);
        return true;
    }

    public async Task<bool> IsChannelWatchedAsync(string guildId, string channelId)
    {
        var config = await GetByKeyAsync(guildId);
        return config?.WatchedChannels.Contains(channelId) ?? false;
    }

    public async Task<List<string>> GetWatchedChannelsAsync(string guildId)
    {
        var config = await GetByKeyAsync(guildId);
        return config?.WatchedChannels ?? new List<string>();
    }
}