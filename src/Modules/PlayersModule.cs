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
            if (notif.TryGetParams<Player[]>(out var list))
                PlayerJoined?.Invoke(list![0]);              
        });

        client.SetNotificationEvent("minecraft:notification/players/left", notif =>
        {
            if (notif.TryGetParams<Player[]>(out var list))
                PlayerLeft?.Invoke(list![0]);
        });
    }

    /// <summary>
    /// An event that is invoked when a player joins the server.
    /// </summary>
    public event Action<Player>? PlayerJoined;

    /// <summary>
    /// An event that is invoked when a player leaves the server.
    /// </summary>
    public event Action<Player>? PlayerLeft;

    /// <summary>
    /// Gets all connected list.
    /// </summary>
    /// <returns>An array of list.</returns>
    public async Task<Player[]> GetAsync() => await client.CallMethodAsync<Player[]>("minecraft:players");

    /// <summary>
    /// Kicks list from the server.
    /// </summary>
    /// <param name="kickPlayers">An array of kick data objects.</param>
    /// <returns>An array of kicked list.</returns>
    public async Task<Player[]> KickAsync(KickPlayer[] kickPlayers) => await client.CallMethodAsync<Player[]>("minecraft:players/kick", [kickPlayers]);
}