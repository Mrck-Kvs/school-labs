# SocialGraph Bot

Bot Discord qui analyse les interactions sociales entre membres d'un serveur via les @mentions et construit un graphe de relations pondéré.

---

## Prérequis

- [.NET 10.0 SDK](https://dotnet.microsoft.com/fr-fr/download/dotnet/10.0)
- [Docker](https://www.docker.com/get-started/) et Docker Compose
- Un [Bot Discord](https://discord.com/developers/applications) avec son token

---

## Installation

### 1. Cloner le repository

```bash
git clone https://gitlab.ictge.ch/kevin-frrzr/socialgraph-bot.git
cd socialgraph-bot
```

### 2. Lancer ArangoDB avec Docker Compose

```bash
docker-compose up -d
```

ArangoDB sera accessible sur :
- **Interface Web** : http://localhost:8529
- **Credentials par défaut** : `root` / `rootpassword`

### 3. Configurer la base de données

1. Ouvrir http://localhost:8529
2. Se connecter avec `root` / `rootpassword`
3. Créer une nouvelle base de données : `socialgraph`
4. Dans cette base, créer les collections :
   - `users` (Document Collection) Cette collection stocke toutes les infos utiles des utilisateurs.
   - `interactions` (Edge Collection) Les interactions sont le nombre de mentions (pondération) qui est `from_` un user et `to_` un autre user.
   - `config` (Document Collection) Cette collection permet de stocker les channels que le bot doit écouter.

### 4. Configurer le Bot

Copier le fichier de configuration exemple :

```bash
cp appsettings.example.json appsettings.json
```

Éditer `appsettings.json` avec vos credentials :

```json
{
  "Discord": {
    "Token": "VOTRE_TOKEN_DISCORD_ICI"
  },
  "ArangoDB": {
    "Url": "http://localhost:8529",
    "Database": "socialgraph",
    "Username": "root",
    "Password": "rootpassword"
  }
}
```

Puis mettez le dans le dossier `SocialGrapheBot/bin/Debug/net10.0/`

### 5. Lancer le Bot

```bash
dotnet restore
dotnet run
```

---

## Docker Compose

Le fichier `docker-compose.yml` configure ArangoDB :

```yaml
version: '3.8'

services:
  arangodb:
    image: arangodb:latest
    container_name: socialgraph-arangodb
    environment:
      ARANGO_ROOT_PASSWORD: rootpassword
    ports:
      - "8529:8529"
    volumes:
      - arangodb_data:/var/lib/arangodb3
      - arangodb_apps:/var/lib/arangodb3-apps
    restart: unless-stopped

volumes:
  arangodb_data:
  arangodb_apps:
```

### Commandes utiles

```bash
# Démarrer ArangoDB
docker-compose up -d

# Voir les logs
docker-compose logs -f arangodb

# Arrêter ArangoDB
docker-compose down

# Arrêter et supprimer les données
docker-compose down -v
```

---

## Dépendances .NET

| Package | Version | Description |
|---------|---------|-------------|
| [Discord.NET](https://github.com/discord-net/Discord.Net) | 3.18.0 | API Discord pour C# |
| [ArangoDBNetStandard](https://github.com/ArangoDB-Community/arangodb-net-standard) | Latest | Driver officiel ArangoDB avec support JWT |
| Microsoft.Extensions.Configuration | 10.0.2 | Gestion de la configuration |
| Microsoft.Extensions.DependencyInjection | 10.0.2 | Injection de dépendances |

### Installation des packages

```bash
dotnet add package Discord.Net
dotnet add package ArangoDBNetStandard
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
```

---

## Structure du Projet

```
SocialGraphBot/
├── Commands/
│   ├── CheminCommand.cs      → /chemin @user (SHORTEST_PATH)
│   ├── ConfigCommand.cs      → /config add|remove|list (Admin)
│   ├── ProfileCommand.cs     → /profil [@user]
│   ├── RelationsCommand.cs   → /relations
│   ├── StatsCommand.cs       → /stats (Admin)
│   └── TopCommand.cs         → /top
├── Handlers/
│   └── MentionHandler.cs     → Tracking temps réel des @mentions
├── Models/
│   ├── User.cs
│   ├── Interaction.cs        → Edge avec weight
│   └── GuildConfig.cs
├── Repositories/
│   ├── ArangoDbContext.cs    → Connexion + requêtes AQL
│   ├── UserRepository.cs
│   ├── InteractionRepository.cs → SHORTEST_PATH, traversée graphe
│   └── ConfigRepository.cs
├── Services/
│   ├── UserService.cs
│   ├── InteractionService.cs
│   └── ConfigService.cs
├── Program.cs                → DI + bootstrap
├── SocialGraphBot.csproj
└── appsettings.json
```

---

## Configuration ArangoDB

### Collections

#### `users` (Document Collection)
```json
{
  "_key": "123456789012345678", // Discord id
  "username": "Alice", // Discord username
  "joinedAt": "2024-01-15T10:30:00Z", // joined the server
  "totalMentionsSent": 150,
  "totalMentionsReceived": 342
}
```

#### `interactions` (Edge Collection)
```json
{
  "_from": "users/123456789012345678", // from the user A
  "_to": "users/987654321098765432", // to the user B
  "weight": 127, // number of mentions
  "firstInteraction": "2024-01-20T14:00:00Z",
  "lastInteraction": "2024-02-10T09:15:00Z"
}
```

#### `config` (Document Collection)
```json
{
  "_key": "111111111111111111", // guild id
  "guildName": "Mon Serveur", // guild name
  "watchedChannels": ["222222222222222222", "333333333333333333"], // channel setted
  "createdAt": "2024-01-15T10:00:00Z"
}
```

---

## Commandes du Bot

### Utilisateur

| Commande | Description |
|----------|-------------|
| `/profil` | Affiche ton profil social |
| `/profil @user` | Affiche le profil d'un autre utilisateur |
| `/relations` | Liste toutes tes connexions |
| `/chemin @user` | Trouve le chemin entre toi et un autre user |
| `/top` | Classement des 10 meilleures réputations |

### Admin

| Commande | Description |
|----------|-------------|
| `/config add #channel` | Ajoute un channel à surveiller |
| `/config remove #channel` | Retire un channel de la surveillance |
| `/config list` | Liste les channels surveillés |
| `/stats` | Statistiques globales du serveur |

---

## Requêtes AQL Utiles

### Plus court chemin entre deux users
```aql
FOR v, e IN OUTBOUND SHORTEST_PATH 
  'users/123456789' TO 'users/987654321' 
  interactions
  RETURN { vertex: v, edge: e }
```

### Top 10 par réputation
```aql
FOR u IN users
  SORT u.totalMentionsReceived DESC
  LIMIT 10
  RETURN { username: u.username, reputation: u.totalMentionsReceived }
```

### Connexions d'un user
```aql
FOR v, e IN 1..1 OUTBOUND 'users/123456789' interactions
  LET level = (
    e.weight >= 100 ? "Ami proche" :
    e.weight >= 50 ? "Ami" :
    e.weight >= 10 ? "Connaissance" : "Inconnu"
  )
  SORT e.weight DESC
  RETURN { user: v.username, weight: e.weight, level: level }
```

---

## Données de Test

Des données fictives peuvent être importées pour tester le bot sans serveur Discord actif.

```bash
# Importer les données de test
cd scripts
./import-test-data.sh
```

Voir le dossier `test-data/` pour les fichiers JSON.

---

## Licence

Projet académique - Data Management

---

##  Auteur

**Mrck-Kvs**