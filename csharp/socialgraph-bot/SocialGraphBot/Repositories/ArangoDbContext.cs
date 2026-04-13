using ArangoDBNetStandard;
using ArangoDBNetStandard.Transport.Http;

namespace SocialGraphBot.Repositories;

/// <summary>
/// Gère la connexion à ArangoDB et fournit le client
/// </summary>
public class ArangoDbContext : IDisposable
{
    private readonly ArangoDBClient _client;
    private readonly string _database;
    private bool _disposed;

    public ArangoDBClient Client => _client;
    public string Database => _database;

    // Noms des collections
    public const string UsersCollection = "users";
    public const string InteractionsCollection = "interactions";
    public const string ConfigCollection = "config";

    public ArangoDbContext(string url, string database, string username, string password)
    {
        _database = database;

        var transport = HttpApiTransport.UsingBasicAuth(
            new Uri(url),
            database,
            username,
            password);

        _client = new ArangoDBClient(transport);
    }

    /// <summary>
    /// Initialise les collections si elles n'existent pas
    /// </summary>
    public async Task InitializeAsync()
    {
        var collections = await _client.Collection.GetCollectionsAsync();
        var existingNames = collections.Result.Select(c => c.Name).ToHashSet();

        // Collection users (document)
        if (!existingNames.Contains(UsersCollection))
        {
            await _client.Collection.PostCollectionAsync(
                new ArangoDBNetStandard.CollectionApi.Models.PostCollectionBody
                {
                    Name = UsersCollection,
                    Type = ArangoDBNetStandard.CollectionApi.Models.CollectionType.Document
                });
            Console.WriteLine($"✓ Collection '{UsersCollection}' créée");
        }

        // Collection interactions (edge)
        if (!existingNames.Contains(InteractionsCollection))
        {
            await _client.Collection.PostCollectionAsync(
                new ArangoDBNetStandard.CollectionApi.Models.PostCollectionBody
                {
                    Name = InteractionsCollection,
                    Type = ArangoDBNetStandard.CollectionApi.Models.CollectionType.Edge
                });
            Console.WriteLine($"✓ Collection '{InteractionsCollection}' créée");
        }

        // Collection config (document)
        if (!existingNames.Contains(ConfigCollection))
        {
            await _client.Collection.PostCollectionAsync(
                new ArangoDBNetStandard.CollectionApi.Models.PostCollectionBody
                {
                    Name = ConfigCollection,
                    Type = ArangoDBNetStandard.CollectionApi.Models.CollectionType.Document
                });
            Console.WriteLine($"✓ Collection '{ConfigCollection}' créée");
        }
    }

    /// <summary>
    /// Exécute une requête AQL et retourne les résultats
    /// </summary>
    public async Task<List<T>> ExecuteAqlAsync<T>(string aql, Dictionary<string, object>? bindVars = null)
    {
        var response = await _client.Cursor.PostCursorAsync<T>(aql, bindVars);
        return response.Result.ToList();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _client?.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}
