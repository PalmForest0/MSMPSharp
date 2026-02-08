using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class PlayersModule : ModuleBase
{
    internal PlayersModule(MsmpClient client) : base(client)
    {
        client.SetNotificationEvent("minecraft:notification/players/joined", notif =>
        {
            if (notif.TryGetParams<Player[]>(out var players))
                PlayerJoined?.Invoke(players![0]);              
        });
    }

    public event Action<Player>? PlayerJoined;

    /// <summary>
    /// Gets all connected players.
    /// </summary>
    /// <returns>An array of players.</returns>
    public async Task<Player[]> GetAsync() => await client.CallMethodAsync<Player[]>("minecraft:players");

    /// <summary>
    /// Kicks players from the server.
    /// </summary>
    /// <param name="kickPlayers">An array of kick data objects.</param>
    /// <returns>An array of kicked players.</returns>
    public async Task<Player[]> KickAsync(KickPlayer[] kickPlayers) => await client.CallMethodAsync<Player[]>("minecraft:players/kick", [kickPlayers]);
}