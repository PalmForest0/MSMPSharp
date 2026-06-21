using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class PlayersModule : ModuleBase
{
    internal PlayersModule(MsmpClient client) : base(client)
    {
        client.SetNotificationHandler("minecraft:notification/players/joined", notif =>
        {
            if (notif.TryGetParams<Player[]>(out var list))
                PlayerJoined?.Invoke(list![0]);
        });

        client.SetNotificationHandler("minecraft:notification/players/left", notif =>
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
    /// Gets all connected players filtered with an optional condition.
    /// </summary>
    /// <returns>An array of players that meet the condition.</returns>
    public async Task<Player[]> GetAsync(Func<Player, bool>? condition = null)
    {
        var players = await client.SendAsync<Player[]>("minecraft:players");
        return condition is null ? players : players.Where(condition).ToArray();
    }

    /// <summary>
    /// Gets the first connected player that meets the condition.
    /// </summary>
    /// <returns>The first player that meets the condition, otherwise null.</returns>
    public async Task<Player?> GetFirstAsync(Func<Player, bool> condition)
    {
        if (condition is null)
            return null;

        var players = (await client.SendAsync<Player[]>("minecraft:players")).Where(condition).ToArray();
        return players.Length == 0 ? null : players[0];
    }

    /// <summary>
    /// Kicks players from the server.
    /// </summary>
    /// <param name="kickPlayers">An array of player kick data objects.</param>
    /// <returns>An array of kicked players.</returns>
    public async Task<Player[]> KickAsync(KickPlayer[] kickPlayers) => await client.SendAsync<Player[]>("minecraft:players/kick", [kickPlayers]);

    /// <summary>
    /// Kicks a player from the server.
    /// </summary>
    /// <param name="kickPlayer">A player kick data object.</param>
    /// <returns>An array of kicked players.</returns>
    public async Task<Player[]> KickAsync(KickPlayer kickPlayer) => await KickAsync([kickPlayer]);
}