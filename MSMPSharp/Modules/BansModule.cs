using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public class BansModule
{
    private readonly MsmpClient _client;

    internal BansModule(MsmpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets the server's ban list.
    /// </summary>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> GetAsync() => await _client.CallMethodAsync<UserBan[]>("minecraft:bans");

    /// <summary>
    /// Sets the server's ban list.
    /// </summary>
    /// <param name="bans">An array of user bans to set the ban list to.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> SetAsync(UserBan[] bans) => await _client.CallMethodAsync<UserBan[]>("minecraft:bans/set", [bans]);

    /// <summary>
    /// Adds players to the server's ban list.
    /// </summary>
    /// <param name="bans">An array of user bans to add to the ban list.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> AddAsync(UserBan[] bans) => await _client.CallMethodAsync<UserBan[]>("minecraft:bans/add", [bans]);

    /// <summary>
    /// Removes players from the server's ban list.
    /// </summary>
    /// <param name="players">An array of players to remove from the server's ban list.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> RemoveAsync(Player[] players) => await _client.CallMethodAsync<UserBan[]>("minecraft:bans/remove", [players]);

    /// <summary>
    /// Clears all players from the server's ban list.
    /// </summary>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> ClearAsync() => await _client.CallMethodAsync<UserBan[]>("minecraft:bans/clear");
}