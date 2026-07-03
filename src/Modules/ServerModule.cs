using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class ServerModule : ModuleBase
{
    internal ServerModule(MsmpClient client) : base(client) { }

    internal override void RegisterNotificationHandlers()
    {
        _client.SetNotificationHandler("minecraft:notification/server/started", _ => Started?.Invoke(this, EventArgs.Empty));
        _client.SetNotificationHandler("minecraft:notification/server/stopping", _ => Stopping?.Invoke(this, EventArgs.Empty));
        _client.SetNotificationHandler("minecraft:notification/server/saving", _ => Saving?.Invoke(this, EventArgs.Empty));
        _client.SetNotificationHandler("minecraft:notification/server/saved", _ => Saved?.Invoke(this, EventArgs.Empty));
        _client.SetNotificationHandler("minecraft:notification/server/activity", _ => Activity?.Invoke(this, EventArgs.Empty));

        _client.SetNotificationHandler("minecraft:notification/server/status", notif =>
        {
            if (notif.TryGetParams<ServerState>(out var state))
                Status?.Invoke(this, new ServerStateEventArgs(state));
        });
    }

    /// <summary>
    /// An event that is invoked when the server is started.
    /// </summary>
    public event EventHandler? Started;

    /// <summary>
    /// An event that is invoked when the server is shutting down.
    /// </summary>
    public event EventHandler? Stopping;

    /// <summary>
    /// An event that is invoked when the server save is started.
    /// </summary>
    public event EventHandler? Saving;

    /// <summary>
    /// An event that is invoked when the server save is completed.
    /// </summary>
    public event EventHandler? Saved;

    /// <summary>
    /// An event that is invoked on every server status heartbeat.
    /// </summary>
    public event EventHandler<ServerStateEventArgs>? Status;

    /// <summary>
    /// An event that is invoked when the network connection is initialized.
    /// </summary>
    public event EventHandler? Activity;

    /// <summary>
    /// Gets the server's current status.
    /// </summary>
    /// <returns>The current server state.</returns>
    public async Task<ServerState> GetStatusAsync() => await _client.SendAsync<ServerState>("minecraft:server/status");

    /// <summary>
    /// Saves the server's current state.
    /// </summary>
    /// <param name="flush">Whether the server should flush its memory.</param>
    /// <returns>Whether the server is currently saving its state.</returns>
    public async Task<bool> SaveAsync(bool flush) => await _client.SendAsync<bool>("minecraft:server/save", [flush]);

    /// <summary>
    /// Stops the server.
    /// </summary>
    /// <returns>Whether the server is currently stopping.</returns>
    public async Task<bool> StopAsync() => await _client.SendAsync<bool>("minecraft:server/stop");

    /// <summary>
    /// Sends a system message to the server.
    /// </summary>
    /// <returns>Whether the system message was sent.</returns>
    public async Task<bool> SendSystemMessageAsync(SystemMessage message) => await _client.SendAsync<bool>("minecraft:server/system_message", [message]);
}