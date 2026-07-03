using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Game;

namespace MSMPSharp.Modules;

public sealed class AllowlistModule : ModuleBase
{
    internal AllowlistModule(MsmpClient client) : base(client) { }

    internal override void RegisterNotificationHandlers()
    {
        _client.SetNotificationHandler("minecraft:notification/allowlist/added", notif =>
        {
            if (notif.TryGetParams<Player>(out var player))
                PlayerAdded?.Invoke(this, new PlayerEventArgs(player));
        });

        _client.SetNotificationHandler("minecraft:notification/allowlist/removed", notif =>
        {
            if (notif.TryGetParams<Player>(out var player))
                PlayerRemoved?.Invoke(this, new PlayerEventArgs(player));
        });
    }

    /// <summary>
    /// An event that is invoked when a player is added to the allowlist.
    /// </summary>
    public event EventHandler<PlayerEventArgs>? PlayerAdded;

    /// <summary>
    /// An event that is invoked when a player is removed from the allowlist.
    /// </summary>
    public event EventHandler<PlayerEventArgs>? PlayerRemoved;

    /// <summary>
    /// Gets the allowlist.
    /// </summary>
    /// <returns>An array of players.</returns>
    public async Task<Player[]> GetAsync() => await _client.SendAsync<Player[]>("minecraft:allowlist");

    /// <summary>
    /// Sets the allowlist to the provided list of players.
    /// </summary>
    /// <param name="players">An array of players to set the allowlist to.</param>
    /// <returns>An array of players representing the new allowlist.</returns>
    public async Task<Player[]> SetAsync(params Player[] players) => await _client.SendAsync<Player[]>("minecraft:allowlist/set", [players]);

    /// <summary>
    /// Adds players to the allowlist.
    /// </summary>
    /// <param name="players">An array of players to add to the allowlist.</param>
    /// <returns>An array of players representing the new allowlist.</returns>
    public async Task<Player[]> AddAsync(params Player[] players) => await _client.SendAsync<Player[]>("minecraft:allowlist/add", [players]);

    /// <summary>
    /// Removes players from the allowlist.
    /// </summary>
    /// <param name="players">An array of players to remove from the allowlist.</param>
    /// <returns>An array of players representing the new allowlist.</returns>
    public async Task<Player[]> RemoveAsync(params Player[] players) => await _client.SendAsync<Player[]>("minecraft:allowlist/remove", [players]);

    /// <summary>
    /// Clears all players from the allowlist.
    /// </summary>
    /// <returns>An array of players representing the new allowlist.</returns>
    public async Task<Player[]> ClearAsync() => await _client.SendAsync<Player[]>("minecraft:allowlist/clear");
}