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
    /// Gets all connected players filtered with an optional condition.
    /// </summary>
    /// <returns>An array of players that meet the condition.</returns>
    public async Task<Player[]> GetAsync(Func<Player, bool>? condition = null)
    {
        var players = await _client.SendAsync<Player[]>("minecraft:players");
        return condition is null ? players : players.Where(condition).ToArray();
    }

    /// <summary>
    /// Gets the first connected player that meets the condition.
    /// </summary>
    /// <returns>The first player that meets the condition, otherwise null.</returns>
    public async Task<Player?> GetFirstAsync(Func<Player, bool> condition)
    {
        if (condition is null)
            return null;

        var players = (await _client.SendAsync<Player[]>("minecraft:players")).Where(condition).ToArray();
        return players.Length == 0 ? null : players[0];
    }

    /// <summary>
    /// Kicks players from the server.
    /// </summary>
    /// <param name="kickPlayers">An array of player kick data objects.</param>
    /// <returns>An array of kicked players.</returns>
    public async Task<Player[]> KickAsync(params KickPlayer[] kickPlayers) => await _client.SendAsync<Player[]>("minecraft:players/kick", [kickPlayers]);
}