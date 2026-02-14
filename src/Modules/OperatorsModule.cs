using MSMPSharp.Core;
using MSMPSharp.Data.Game;
using MSMPSharp.Data.Server;

namespace MSMPSharp.Modules;

public sealed class OperatorsModule : ModuleBase
{
    internal OperatorsModule(MsmpClient client) : base(client)
    {
        client.SetNotificationEvent("minecraft:notification/operators/added", notif =>
        {
            if (notif.TryGetParams<Operator[]>(out var list))
                OperatorAdded?.Invoke(list![0]);
        });

        client.SetNotificationEvent("minecraft:notification/operators/removed", notif =>
        {
            if (notif.TryGetParams<Operator[]>(out var list))
                OperatorRemoved?.Invoke(list![0]);
        });
    }

    /// <summary>
    /// An event that is invoked when a player is oped.
    /// </summary>
    public event Action<Operator>? OperatorAdded;

    /// <summary>
    /// An event that is invoked when a player is deoped.
    /// </summary>
    public event Action<Operator>? OperatorRemoved;

    /// <summary>
    /// Gets all OPed list on the server.
    /// </summary>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> GetAsync() => await client.CallMethodAsync<Operator[]>("minecraft:operators");

    /// <summary>
    /// Sets all OPed list on the server.
    /// </summary>
    /// <param name="operators">An array of operators to set the server's operators to.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> SetAsync(Operator[] operators) => await client.CallMethodAsync<Operator[]>("minecraft:operators/set", [operators]);

    /// <summary>
    /// Adds operators to the server.
    /// </summary>
    /// <param name="operators">An array of operators to add to the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> AddAsync(Operator[] operators) => await client.CallMethodAsync<Operator[]>("minecraft:operators/add", [operators]);

    /// <summary>
    /// Removes operators from the server.
    /// </summary>
    /// <param name="players">An array of list to deOP from the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> RemoveAsync(Player[] players) => await client.CallMethodAsync<Operator[]>("minecraft:operators/remove", [players]);

    /// <summary>
    /// DeOPs all list on the server.
    /// </summary>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> ClearAsync() => await client.CallMethodAsync<Operator[]>("minecraft:operators/clear");
}