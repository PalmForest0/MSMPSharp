using MSMPSharp.Core;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class ServerModule : ModuleBase
{
    internal ServerModule(MsmpClient client) : base(client) { }

    /// <summary>
    /// Gets the server's current status.
    /// </summary>
    /// <returns>The current server state.</returns>
    public async Task<ServerState> GetStatusAsync() => await client.CallMethodAsync<ServerState>("minecraft:server/status");

    /// <summary>
    /// Saves the server's current state.
    /// </summary>
    /// <param name="flush">Whether the server should flush its memory.</param>
    /// <returns>Whether the server is currently saving its state.</returns>
    public async Task<bool> SaveAsync(bool flush) => await client.CallMethodAsync<bool>("minecraft:server/save", [flush]);

    /// <summary>
    /// Stops the server.
    /// </summary>
    /// <returns>Whether the server is currently stopping.</returns>
    public async Task<bool> StopAsync() => await client.CallMethodAsync<bool>("minecraft:server/stop");

    /// <summary>
    /// Sends a system message to the server.
    /// </summary>
    /// <returns>Whether the system message was sent.</returns>
    public async Task<bool> SendSystemMessageAsync(SystemMessage message) => await client.CallMethodAsync<bool>("minecraft:server/system_message", [message]);
}