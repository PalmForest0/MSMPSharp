using MSMPSharp.Core;
using MSMPSharp.Data.Server;

namespace MSMPSharp.Modules;

public sealed class IpBansModule : ModuleBase
{
    internal IpBansModule(MsmpClient client) : base(client)
    {
        client.SetNotificationEvent("minecraft:notification/ip_bans/added", notif =>
        {
            if (notif.TryGetParams<IpBan[]>(out var list))
                IpBanAdded?.Invoke(list![0]);
        });

        client.SetNotificationEvent("minecraft:notification/ip_bans/removed", notif =>
        {
            if (notif.TryGetParams<string[]>(out var list))
                IpBanRemoved?.Invoke(list![0]);
        });
    }

    /// <summary>
    /// An event that is invoked when a player is added to the allowlist.
    /// </summary>
    public event Action<IpBan>? IpBanAdded;

    /// <summary>
    /// An event that is invoked when an IP is removed from the ip-ban list.
    /// <para><see langword="string"/> param - Removed Ip.</para>
    /// </summary>
    public event Action<string>? IpBanRemoved;

    /// <summary>
    /// Gets the server's IP ban list.
    /// </summary>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> GetAsync() => await client.CallMethodAsync<IpBan[]>("minecraft:ip_bans");

    /// <summary>
    /// Sets the server's IP ban list.
    /// </summary>
    /// <param name="bans">An array of IP bans to set the ban list to.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> SetAsync(IpBan[] bans) => await client.CallMethodAsync<IpBan[]>("minecraft:ip_bans/set", [bans]);

    /// <summary>
    /// Adds players to the server's IP ban list.
    /// </summary>
    /// <param name="bans">An array of incoming IP bans to add to the IP ban list.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> AddAsync(IncomingIpBan[] bans) => await client.CallMethodAsync<IpBan[]>("minecraft:ip_bans/add", [bans]);

    /// <summary>
    /// Removes players from the server's IP ban list.
    /// </summary>
    /// <param name="ips">An array of IPs to remove from the server's IP ban list.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> RemoveAsync(string[] ips) => await client.CallMethodAsync<IpBan[]>("minecraft:ip_bans/remove", [ips]);

    /// <summary>
    /// Clears all IPs from the server's IP ban list.
    /// </summary>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> ClearAsync() => await client.CallMethodAsync<IpBan[]>("minecraft:ip_bans/clear");
}