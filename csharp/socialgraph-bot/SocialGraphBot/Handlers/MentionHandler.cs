using Discord.Interactions;
using Discord.WebSocket;
using SocialGraphBot.Services;

namespace SocialGraphBot.Handlers;

/// <summary>
/// Handler pour l'événement MessageReceived
/// Détecte les @mentions et les enregistre dans le graphe
/// Implémente UC1 et les règles R1, R2, R3, R4
/// </summary>
public class MentionHandler
{
    private readonly DiscordSocketClient _client;
    private readonly Services.InteractionService _interactionService;
    private readonly ConfigService _configService;

    public MentionHandler(
        DiscordSocketClient client,
        Services.InteractionService interactionService,
        ConfigService configService)
    {
        _client = client;
        _interactionService = interactionService;
        _configService = configService;
    }

    /// <summary>
    /// Initialise le handler en s'abonnant à l'événement
    /// </summary>
    public void Initialize()
    {
        _client.MessageReceived += HandleMessageAsync;
    }

    /// <summary>
    /// Traite chaque message reçu pour détecter les mentions
    /// </summary>
    private async Task HandleMessageAsync(SocketMessage message)
    {
        // Ignorer les messages de bots
        if (message.Author.IsBot)
            return;

        // S'assurer que c'est un message utilisateur dans une guild
        if (message is not SocketUserMessage userMessage)
            return;

        if (message.Channel is not SocketTextChannel textChannel)
            return;

        var guild = textChannel.Guild;
        var guildId = guild.Id;
        var channelId = textChannel.Id;

        // R2: Vérifier si le channel est surveillé
        var isWatched = await _configService.IsChannelWatchedAsync(guildId, channelId);
        if (!isWatched)
            return;

        // Récupérer les mentions utilisateur valides
        // R1: Seules les mentions @user sont comptées (pas @everyone, @here, @role)
        var validMentions = userMessage.MentionedUsers
            .Where(u => !u.IsBot) // Ignorer les bots
            .Where(u => u.Id != message.Author.Id) // R4: Ignorer auto-mentions
            .Distinct() // Éviter les doublons dans le même message
            .ToList();

        if (!validMentions.Any())
            return;

        // Récupérer les infos de l'auteur
        var authorGuildUser = guild.GetUser(message.Author.Id);
        var authorJoinedAt = authorGuildUser?.JoinedAt?.UtcDateTime;

        // R3: Un message avec plusieurs @user crée/update plusieurs edges
        foreach (var mentionedUser in validMentions)
        {
            var mentionedGuildUser = guild.GetUser(mentionedUser.Id);
            var mentionedJoinedAt = mentionedGuildUser?.JoinedAt?.UtcDateTime;

            try
            {
                await _interactionService.ProcessMentionAsync(
                    guildId,
                    channelId,
                    message.Author.Id,
                    message.Author.Username,
                    authorJoinedAt,
                    mentionedUser.Id,
                    mentionedUser.Username,
                    mentionedJoinedAt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du traitement de la mention: {ex.Message}");
            }
        }
    }
}
