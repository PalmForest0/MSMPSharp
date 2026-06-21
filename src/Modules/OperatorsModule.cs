using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class OperatorsModule : ModuleBase
{
    internal OperatorsModule(MsmpClient client) : base(client)
    {
        client.SetNotificationHandler("minecraft:notification/operators/added", notif =>
        {
            if (notif.TryGetParams<Operator[]>(out var list))
                OperatorAdded?.Invoke(list![0]);
        });

        client.SetNotificationHandler("minecraft:notification/operators/removed", notif =>
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
    public async Task<Operator[]> GetAsync() => await client.SendAsync<Operator[]>("minecraft:operators");

    /// <summary>
    /// Sets all OPed list on the server.
    /// </summary>
    /// <param name="operators">An array of operators to set the server's operators to.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> SetAsync(Operator[] operators) => await client.SendAsync<Operator[]>("minecraft:operators/set", [operators]);

    /// <summary>
    /// Adds operators to the server.
    /// </summary>
    /// <param name="operators">An array of operators to add to the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> AddAsync(Operator[] operators) => await client.SendAsync<Operator[]>("minecraft:operators/add", [operators]);

    /// <summary>
    /// Removes operators from the server.
    /// </summary>
    /// <param name="players">An array of list to deOP from the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> RemoveAsync(Player[] players) => await client.SendAsync<Operator[]>("minecraft:operators/remove", [players]);

    /// <summary>
    /// DeOPs all list on the server.
    /// </summary>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> ClearAsync() => await client.SendAsync<Operator[]>("minecraft:operators/clear");
}