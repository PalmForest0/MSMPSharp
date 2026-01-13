using MSMPSharp.Core;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public class IpBansModule
{
    private readonly MsmpClient _client;

    internal IpBansModule(MsmpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets the server's IP ban list.
    /// </summary>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> GetAsync() => await _client.CallMethodAsync<IpBan[]>("minecraft:ip_bans");

    /// <summary>
    /// Sets the server's IP ban list.
    /// </summary>
    /// <param name="bans">An array of IP bans to set the ban list to.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> SetAsync(IpBan[] bans) => await _client.CallMethodAsync<IpBan[]>("minecraft:ip_bans/set", [bans]);

    /// <summary>
    /// Adds players to the server's IP ban list.
    /// </summary>
    /// <param name="bans">An array of incoming IP bans to add to the IP ban list.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> AddAsync(IncomingIpBan[] bans) => await _client.CallMethodAsync<IpBan[]>("minecraft:ip_bans/add", [bans]);

    /// <summary>
    /// Removes players from the server's IP ban list.
    /// </summary>
    /// <param name="ips">An array of IPs to remove from the server's IP ban list.</param>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> RemoveAsync(string[] ips) => await _client.CallMethodAsync<IpBan[]>("minecraft:ip_bans/remove", [ips]);

    /// <summary>
    /// Clears all IPs from the server's IP ban list.
    /// </summary>
    /// <returns>An array of the server's IP bans.</returns>
    public async Task<IpBan[]> ClearAsync() => await _client.CallMethodAsync<IpBan[]>("minecraft:ip_bans/clear");
}