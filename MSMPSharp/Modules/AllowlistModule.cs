using MSMPSharp.Core;
using MSMPSharp.Models.Game;

namespace MSMPSharp.Modules;

public class AllowlistModule
{
    private readonly MsmpClient _client;

    internal AllowlistModule(MsmpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets the allowlist.
    /// </summary>
    /// <returns>An array of players.</returns>
    public async Task<Player[]> GetAsync() => await _client.CallMethodAsync<Player[]>("minecraft:allowlist");

    /// <summary>
    /// Sets the allowlist to the provided list of players.
    /// </summary>
    /// <param name="players">An array of players to set the allowlist to.</param>
    /// <returns>An array of players representing the new allowlist.</returns>
    public async Task<Player[]> SetAsync(Player[] players) => await _client.CallMethodAsync<Player[]>("minecraft:allowlist/set", [ players ]);

    /// <summary>
    /// Adds players to the allowlist.
    /// </summary>
    /// <param name="players">An array of players to add to the allowlist.</param>
    /// <returns>An array of players representing the new allowlist.</returns>
    public async Task<Player[]> AddAsync(Player[] players) => await _client.CallMethodAsync<Player[]>("minecraft:allowlist/add", [ players ]);

    /// <summary>
    /// Removes players from the allowlist.
    /// </summary>
    /// <param name="players">An array of players to remove from the allowlist.</param>
    /// <returns>An array of players representing the new allowlist.</returns>
    public async Task<Player[]> RemoveAsync(Player[] players) => await _client.CallMethodAsync<Player[]>("minecraft:allowlist/remove", [ players ]);

    /// <summary>
    /// Clears all players from the allowlist.
    /// </summary>
    /// <returns>An array of players representing the new allowlist.</returns>
    public async Task<Player[]> ClearAsync() => await _client.CallMethodAsync<Player[]>("minecraft:allowlist/clear");
}