using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class ServerModule : ModuleBase
{
    internal ServerModule(MsmpClient client) : base(client)
    {
        client.SetNotificationHandler("minecraft:notification/server/started", _ => Started?.Invoke());
        client.SetNotificationHandler("minecraft:notification/server/stopping", _ => Stopping?.Invoke());
        client.SetNotificationHandler("minecraft:notification/server/saving", _ => Saving?.Invoke());
        client.SetNotificationHandler("minecraft:notification/server/saved", _ => Saved?.Invoke());
        client.SetNotificationHandler("minecraft:notification/server/activity", _ => Activity?.Invoke());

        client.SetNotificationHandler("minecraft:notification/server/status", notif =>
        {
            if (notif.TryGetParams<ServerState>(out var state))
                Status?.Invoke(this, new ServerStateEventArgs(state));
        });
    }

    /// <summary>
    /// An event that is invoked when the server is started.
    /// </summary>
    public event Action? Started;

    /// <summary>
    /// An event that is invoked when the server is shutting down.
    /// </summary>
    public event Action? Stopping;

    /// <summary>
    /// An event that is invoked when the server save is started.
    /// </summary>
    public event Action? Saving;

    /// <summary>
    /// An event that is invoked when the server save is completed.
    /// </summary>
    public event Action? Saved;

    /// <summary>
    /// An event that is invoked on every server status heartbeat.
    /// </summary>
    public event EventHandler<ServerStateEventArgs>? Status;

    /// <summary>
    /// An event that is invoked when the network connection is initialized.
    /// </summary>
    public event Action? Activity;

    /// <summary>
    /// Gets the server's current status.
    /// </summary>
    /// <returns>The current server state.</returns>
    public async Task<ServerState> GetStatusAsync() => await client.SendAsync<ServerState>("minecraft:server/status");

    /// <summary>
    /// Saves the server's current state.
    /// </summary>
    /// <param name="flush">Whether the server should flush its memory.</param>
    /// <returns>Whether the server is currently saving its state.</returns>
    public async Task<bool> SaveAsync(bool flush) => await client.SendAsync<bool>("minecraft:server/save", [flush]);

    /// <summary>
    /// Stops the server.
    /// </summary>
    /// <returns>Whether the server is currently stopping.</returns>
    public async Task<bool> StopAsync() => await client.SendAsync<bool>("minecraft:server/stop");

    /// <summary>
    /// Sends a system message to the server.
    /// </summary>
    /// <returns>Whether the system message was sent.</returns>
    public async Task<bool> SendSystemMessageAsync(SystemMessage message) => await client.SendAsync<bool>("minecraft:server/system_message", [message]);
}