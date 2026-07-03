using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class BansModule : ModuleBase
{
    internal BansModule(MsmpClient client) : base(client) { }

    internal override void RegisterNotificationHandlers()
    {
        _client.SetNotificationHandler("minecraft:notification/bans/added", notif =>
        {
            if (notif.TryGetParams<UserBan>(out var ban))
                BanAdded?.Invoke(this, new BanEventArgs(ban));
        });

        _client.SetNotificationHandler("minecraft:notification/bans/removed", async notif =>
        {
            if (notif.TryGetParams<Player>(out var player))
            {
                var allBans = await GetAsync();
                if (allBans.FirstOrDefault(ban => ban.Player == player) is UserBan ban)
                    BanRemoved?.Invoke(this, new BanEventArgs(ban));
            }
        });
    }

    /// <summary>
    /// An event that is invoked when a player is added to the ban list.
    /// </summary>
    public event EventHandler<BanEventArgs>? BanAdded;

    /// <summary>
    /// An event that is invoked when a player is removed from the ban list.
    /// </summary>
    public event EventHandler<BanEventArgs>? BanRemoved;

    /// <summary>
    /// Gets the server's ban list.
    /// </summary>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> GetAsync() => await _client.SendAsync<UserBan[]>("minecraft:bans");

    /// <summary>
    /// Sets the server's ban list.
    /// </summary>
    /// <param name="bans">An array of user bans to set the ban list to.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> SetAsync(params UserBan[] bans) => await _client.SendAsync<UserBan[]>("minecraft:bans/set", [bans]);

    /// <summary>
    /// Adds players to the server's ban list.
    /// </summary>
    /// <param name="bans">An array of user bans to add to the ban list.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> AddAsync(params UserBan[] bans) => await _client.SendAsync<UserBan[]>("minecraft:bans/add", [bans]);

    /// <summary>
    /// Removes players from the server's ban list.
    /// </summary>
    /// <param name="bans">An array of user bans to remove from the server's ban list.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> RemoveAsync(params UserBan[] bans) => await _client.SendAsync<UserBan[]>("minecraft:bans/remove", [bans.Select(ban => ban.Player)]);

    /// <summary>
    /// Removes players from the server's ban list.
    /// </summary>
    /// <param name="players">An array of players to remove from the server's ban list.</param>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> RemoveAsync(params Player[] players) => await _client.SendAsync<UserBan[]>("minecraft:bans/remove", [players]);

    /// <summary>
    /// Clears all players from the server's ban list.
    /// </summary>
    /// <returns>An array of the server's user bans.</returns>
    public async Task<UserBan[]> ClearAsync() => await _client.SendAsync<UserBan[]>("minecraft:bans/clear");
}