using MSMPSharp.Core;
using MSMPSharp.Models.Game;

namespace MSMPSharp.Modules;

public class GamerulesModule
{
    private readonly MsmpClient _client;

    internal GamerulesModule(MsmpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Get the available game rule keys and their current values.
    /// </summary>
    /// <returns>An array of available game rules.</returns>
    public async Task<TypedGameRule[]> GetAsync() => await _client.CallMethodAsync<TypedGameRule[]>("minecraft:gamerules");

    /// <summary>
    /// Updates the value of a game rule.
    /// </summary>
    /// <returns>The updated game rule.</returns>
    public async Task<TypedGameRule> UpdateAsync(UntypedGameRule gamerule) => await _client.CallMethodAsync<TypedGameRule>("minecraft:gamerules/update", [gamerule]);
}