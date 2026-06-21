using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class IpBansModule : ModuleBase
{
    internal IpBansModule(MsmpClient client) : base(client)
    {
        client.SetNotificationHandler("minecraft:notification/ip_bans/added", notif =>
        {
            if (notif.TryGetParams<IpBan[]>(out var list))
                IpBanAdded?.Invoke(this, new IpBanEventArgs(list![0]));
        });

        client.SetNotificationHandler("minecraft:notification/ip_bans/removed", async notif =>
        {
            if (notif.TryGetParams<string[]>(out var list))
            {
                var allIpBans = await GetAsync();
                if (allIpBans.FirstOrDefault(ban => ban.Ip == list![0]) is IpBan removedBan)
                    IpBanRemoved?.Invoke(this, new IpBanEventArgs(removedBan));
            }
        });
    }

    /// <summary>
    /// An event that is invoked when a player is added to the allowlist.
    /// </summary>
    public event EventHandler<IpBanEventArgs>? IpBanAdded;

    /// <summary>
    /// An event that is invoked when an IP is removed from the ip-ban list.
    /// <para><see langword="string"/> param - Removed Ip.</para>
    /// </summary>
    public event EventHandler<IpBanEventArgs>? IpBanRemoved;

    /// <summary>
    /// Gets the server's IP ban list.
    /// </summary>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> GetAsync() => await client.SendAsync<IpBan[]>("minecraft:ip_bans");

    /// <summary>
    /// Sets the server's IP ban list.
    /// </summary>
    /// <param name="bans">An array of IP bans to set the ban list to.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> SetAsync(params IpBan[] bans) => await client.SendAsync<IpBan[]>("minecraft:ip_bans/set", [bans]);

    /// <summary>
    /// Adds players to the server's IP ban list.
    /// </summary>
    /// <param name="bans">An array of incoming IP bans to add to the IP ban list.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> AddAsync(params IncomingIpBan[] bans) => await client.SendAsync<IpBan[]>("minecraft:ip_bans/add", [bans]);

    /// <summary>
    /// Removes players from the server's IP ban list.
    /// </summary>
    /// <param name="ips">An array of IPs to remove from the server's IP ban list.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> RemoveAsync(params string[] ips) => await client.SendAsync<IpBan[]>("minecraft:ip_bans/remove", [ips]);

    /// <summary>
    /// Clears all IPs from the server's IP ban list.
    /// </summary>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> ClearAsync() => await client.SendAsync<IpBan[]>("minecraft:ip_bans/clear");
}