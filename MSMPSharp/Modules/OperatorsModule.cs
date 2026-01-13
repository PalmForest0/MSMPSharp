using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public class OperatorsModule
{
    private readonly MsmpClient _client;

    internal OperatorsModule(MsmpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets all OPed players on the server.
    /// </summary>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> GetAsync() => await _client.CallMethodAsync<Operator[]>("minecraft:operators");

    /// <summary>
    /// Sets all OPed players on the server.
    /// </summary>
    /// <param name="operators">An array of operators to set the server's operators to.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> SetAsync(Operator[] operators) => await _client.CallMethodAsync<Operator[]>("minecraft:operators/set", [operators]);

    /// <summary>
    /// Adds operators to the server.
    /// </summary>
    /// <param name="operators">An array of operators to add to the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> AddAsync(Operator[] operators) => await _client.CallMethodAsync<Operator[]>("minecraft:operators/add", [operators]);

    /// <summary>
    /// Removes operators from the server.
    /// </summary>
    /// <param name="players">An array of players to deOP from the server.</param>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> RemoveAsync(Player[] players) => await _client.CallMethodAsync<Operator[]>("minecraft:operators/remove", [players]);

    /// <summary>
    /// DeOPs all players on the server.
    /// </summary>
    /// <returns>An array of the server's operators.</returns>
    public async Task<Operator[]> ClearAsync() => await _client.CallMethodAsync<Operator[]>("minecraft:operators/clear");
}