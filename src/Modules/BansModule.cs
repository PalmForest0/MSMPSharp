using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class BansModule : ModuleBase
{
    internal BansModule(MsmpClient client) : base(client)
    {
        client.SetNotificationHandler("minecraft:notification/bans/added", notif =>
        {
            if (notif.TryGetParams<UserBan[]>(out var list))
                BanAdded?.Invoke(list![0]);
        });

        client.SetNotificationHandler("minecraft:notification/bans/removed", notif =>
        {
            if (notif.TryGetParams<Player[]>(out var list))
                BanRemoved?.Invoke(list![0]);
        });
    }

    /// <summary>
    /// An event that is invoked when a player is added to the ban list.
    /// </summary>
    public event Action<UserBan>? BanAdded;

    /// <summary>
    /// An event that is invoked when a player is removed from the ban list.
    /// </summary>
    public event Action<Player>? BanRemoved;

    /// <summary>
    /// Gets the server's ban list.
    /// </summary>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> GetAsync() => await client.SendAsync<UserBan[]>("minecraft:bans");

    /// <summary>
    /// Sets the server's ban list.
    /// </summary>
    /// <param name="bans">An array of user bans to set the ban list to.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> SetAsync(UserBan[] bans) => await client.SendAsync<UserBan[]>("minecraft:bans/set", [bans]);

    /// <summary>
    /// Adds players to the server's ban list.
    /// </summary>
    /// <param name="bans">An array of user bans to add to the ban list.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> AddAsync(UserBan[] bans) => await client.SendAsync<UserBan[]>("minecraft:bans/add", [bans]);

    /// <summary>
    /// Removes players from the server's ban list.
    /// </summary>
    /// <param name="players">An array of players to remove from the server's ban list.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> RemoveAsync(Player[] players) => await client.SendAsync<UserBan[]>("minecraft:bans/remove", [players]);

    /// <summary>
    /// Clears all players from the server's ban list.
    /// </summary>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> ClearAsync() => await client.SendAsync<UserBan[]>("minecraft:bans/clear");
}