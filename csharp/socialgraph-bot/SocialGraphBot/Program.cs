using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialGraphBot.Commands;
using SocialGraphBot.Handlers;
using SocialGraphBot.Repositories;
using SocialGraphBot.Services;

namespace SocialGraphBot;

/// <summary>
/// Point d'entrée du bot SocialGraph
/// Configure l'injection de dépendances et initialise tous les composants
/// </summary>
public class Program
{
    private static IServiceProvider _services = null!;
    private static IConfiguration _config = null!;

    public static async Task Main(string[] args)
    {
        // Charger la configuration
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // Configurer les services
        _services = ConfigureServices();

        // Lancer le bot
        await RunBotAsync();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Configuration Discord
        var discordConfig = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds
                           | GatewayIntents.GuildMessages
                           | GatewayIntents.MessageContent
                           | GatewayIntents.GuildMembers,
            LogLevel = LogSeverity.Info
        };

        // Clients Discord
        services.AddSingleton(discordConfig);
        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton<Discord.Interactions.InteractionService>(sp =>
            new Discord.Interactions.InteractionService(sp.GetRequiredService<DiscordSocketClient>()));

        // ArangoDB Context
        services.AddSingleton<ArangoDbContext>(sp =>
        {
            var url = _config["ArangoDB:Url"] ?? "http://localhost:8529";
            var database = _config["ArangoDB:Database"] ?? "socialgraph";
            var username = _config["ArangoDB:Username"] ?? "root";
            var password = _config["ArangoDB:Password"] ?? "rootpassword";

            return new ArangoDbContext(url, database, username, password);
        });

        // Repositories
        services.AddSingleton<UserRepository>();
        services.AddSingleton<InteractionRepository>();
        services.AddSingleton<ConfigRepository>();

        // Services
        services.AddSingleton<UserService>();
        services.AddSingleton<Services.InteractionService>();
        services.AddSingleton<ConfigService>();

        // Handler
        services.AddSingleton<MentionHandler>();

        return services.BuildServiceProvider();
    }

    private static async Task RunBotAsync()
    {
        var client = _services.GetRequiredService<DiscordSocketClient>();
        var interactionService = _services.GetRequiredService<Discord.Interactions.InteractionService>();
        var dbContext = _services.GetRequiredService<ArangoDbContext>();

        // Logging
        client.Log += LogAsync;
        interactionService.Log += LogAsync;

        // Initialiser la base de données
        Console.WriteLine("🔄 Initialisation de la base de données ArangoDB...");
        try
        {
            await dbContext.InitializeAsync();
            Console.WriteLine("✅ Base de données initialisée avec succès");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erreur lors de l'initialisation de la BDD: {ex.Message}");
            Console.WriteLine("   Vérifiez que ArangoDB est lancé (docker-compose up -d)");
            return;
        }

        // Quand le bot est prêt
        client.Ready += async () =>
        {
            Console.WriteLine($"🤖 {client.CurrentUser.Username} est connecté !");

            // Enregistrer les commandes slash
            Console.WriteLine("🔄 Enregistrement des commandes slash...");
            await interactionService.AddModulesAsync(typeof(Program).Assembly, _services);

            // Enregistrer globalement
            await interactionService.RegisterCommandsToGuildAsync(1467119436216664084);
            Console.WriteLine("✅ Commandes slash enregistrées");

            // Initialiser le handler de mentions
            var mentionHandler = _services.GetRequiredService<MentionHandler>();
            mentionHandler.Initialize();
            Console.WriteLine("✅ Handler de mentions initialisé");
        };

        // Gérer les interactions (slash commands)
        client.InteractionCreated += async interaction =>
        {
            var ctx = new SocketInteractionContext(client, interaction);
            await interactionService.ExecuteCommandAsync(ctx, _services);
        };

        // Se connecter
        var token = _config["Discord:Token"];
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("❌ Token Discord manquant dans appsettings.json");
            return;
        }

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        // Garder le bot en vie
        await Task.Delay(Timeout.Infinite);
    }

    private static Task LogAsync(LogMessage log)
    {
        var severity = log.Severity switch
        {
            LogSeverity.Critical => "CRIT",
            LogSeverity.Error => "ERR ",
            LogSeverity.Warning => "WARN",
            LogSeverity.Info => "INFO",
            LogSeverity.Verbose => "VERB",
            LogSeverity.Debug => "DBG ",
            _ => "????"
        };

        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{severity}] {log.Source}: {log.Message}");

        if (log.Exception != null)
            Console.WriteLine($"           Exception: {log.Exception}");

        return Task.CompletedTask;
    }
}
