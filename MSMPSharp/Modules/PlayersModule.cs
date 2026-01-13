using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public class PlayersModule
{
    private readonly MsmpClient _client;

    internal PlayersModule(MsmpClient client)
    { 
        _client = client; 
    }

    /// <summary>
    /// Gets all connected players.
    /// </summary>
    /// <returns>An array of players.</returns>
    public async Task<Player[]> GetAsync() => await _client.CallMethodAsync<Player[]>("minecraft:players");

    /// <summary>
    /// Kicks players from the server.
    /// </summary>
    /// <param name="kickPlayers">An array of kick data objects.</param>
    /// <returns>An array of kicked players.</returns>
    public async Task<Player[]> KickAsync(KickPlayer[] kickPlayers) => await _client.CallMethodAsync<Player[]>("minecraft:players/kick", [kickPlayers]);
}