using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class OperatorsModule : ModuleBase
{
    internal OperatorsModule(MsmpClient client) : base(client) { }

    internal override void RegisterNotificationHandlers()
    {
        _client.SetNotificationHandler("minecraft:notification/operators/added", notif =>
        {
            if (notif.TryGetParams<Operator>(out var op))
                OperatorAdded?.Invoke(this, new OperatorEventArgs(op));
        });

        _client.SetNotificationHandler("minecraft:notification/operators/removed", notif =>
        {
            if (notif.TryGetParams<Operator>(out var op))
                OperatorRemoved?.Invoke(this, new OperatorEventArgs(op));
        });
    }

    /// <summary>
    /// An event that is invoked when a player is oped.
    /// </summary>
    public event EventHandler<OperatorEventArgs>? OperatorAdded;

    /// <summary>
    /// An event that is invoked when a player is deoped.
    /// </summary>
    public event EventHandler<OperatorEventArgs>? OperatorRemoved;

    /// <summary>
    /// Gets all OPed players on the server.
    /// </summary>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> GetAsync() => await _client.SendAsync<Operator[]>("minecraft:operators");

    /// <summary>
    /// Sets all OPed list on the server.
    /// </summary>
    /// <param name="operators">An array of operators to set the server's operators to.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> SetAsync(params Operator[] operators) => await _client.SendAsync<Operator[]>("minecraft:operators/set", [operators]);

    /// <summary>
    /// Adds operators to the server.
    /// </summary>
    /// <param name="operators">An array of operators to add to the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> AddAsync(params Operator[] operators) => await _client.SendAsync<Operator[]>("minecraft:operators/add", [operators]);

    /// <summary>
    /// Removes operators from the server.
    /// </summary>
    /// <param name="operators">An array of operators to remove from the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> RemoveAsync(params Operator[] operators) => await RemoveAsync(operators.Select(op => op.Player).ToArray());

    /// <summary>
    /// Removes operators from the server.
    /// </summary>
    /// <param name="players">An array of players to deOP from the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> RemoveAsync(params Player[] players) => await _client.SendAsync<Operator[]>("minecraft:operators/remove", [players]);

    /// <summary>
    /// DeOPs all players on the server.
    /// </summary>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> ClearAsync() => await _client.SendAsync<Operator[]>("minecraft:operators/clear");
}