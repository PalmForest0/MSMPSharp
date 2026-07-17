using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class PlayersModule : ModuleBase
{
    internal PlayersModule(MsmpClient client) : base(client) { }

    internal override void RegisterNotificationHandlers()
    {
        _client.SetNotificationHandler("minecraft:notification/players/joined", notif =>
        {
            if (notif.TryGetParams<Player>(out var player))
                PlayerJoined?.Invoke(this, new PlayerEventArgs(player));
        });

        _client.SetNotificationHandler("minecraft:notification/players/left", notif =>
        {
            if (notif.TryGetParams<Player>(out var player))
                PlayerLeft?.Invoke(this, new PlayerEventArgs(player));
        });
    }

    /// <summary>
    /// An event that is invoked when a player joins the server.
    /// </summary>
    public event EventHandler<PlayerEventArgs>? PlayerJoined;

    /// <summary>
    /// An event that is invoked when a player leaves the server.
    /// </summary>
    public event EventHandler<PlayerEventArgs>? PlayerLeft;

    /// <summary>
    /// Gets all connected players with an optional filter condition and maximum results cap.
    /// </summary>
    /// <param name="condition">An optional predicate to filter players by. If null, all players are returned.</param>
    /// <param name="maxResults">An optional cap on the number of results returned. If null, all matches are returned.</param>
    /// <returns>An array of online players that meet the condition.</returns>
    public async Task<Player[]> GetAsync(Func<Player, bool>? condition = null, int? maxResults = null)
    {
        var players = await _client.SendAsync<Player[]>("minecraft:players");
        IEnumerable<Player> result = condition is null ? players : players.Where(condition);
        return maxResults is null ? result.ToArray() : result.Take(maxResults.Value).ToArray();
    }

    /// <summary>
    /// Finds the first online player with the specified name. Returns null if no player is found.
    /// </summary>
    /// <param name="name">The name of the player to find.</param>
    /// <returns>The player if found, otherwise null.</returns>
    public async Task<Player?> FindByNameAsync(string name) => (await GetAsync()).FirstOrDefault(p => p.Name == name);

    /// <summary>
    /// Finds the first online player with the specified Id. Returns null if no player is found.
    /// </summary>
    /// <param name="id">The Id of the player to find.</param>
    /// <returns>The player if found, otherwise null.</returns>
    public async Task<Player?> FindByIdAsync(string id) => (await GetAsync()).FirstOrDefault(p => p.Id == id);

    /// <summary>
    /// Kicks players from the server.
    /// </summary>
    /// <param name="kickPlayers">An array of player kick data objects.</param>
    /// <returns>An array of kicked players.</returns>
    public async Task<Player[]> KickAsync(params KickPlayer[] kickPlayers) => await _client.SendAsync<Player[]>("minecraft:players/kick", [kickPlayers]);
}