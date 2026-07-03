using MSMPSharp.Core;
using MSMPSharp.Models.Server;

namespace MSMPSharp.Modules;

public sealed class ServerSettingsModule : ModuleBase
{
    internal ServerSettingsModule(MsmpClient client) : base(client)
    {
        Autosave                    = new ServerSetting<bool>   (client, "autosave");
        Difficulty                  = new ServerSetting<string> (client, "difficulty");
        EnforceAllowlist            = new ServerSetting<bool>   (client, "enforce_allowlist");
        UseAllowlist                = new ServerSetting<bool>   (client, "use_allowlist");
        MaxPlayers                  = new ServerSetting<int>    (client, "max_players");
        PauseWhenEmptySeconds       = new ServerSetting<int>    (client, "pause_when_empty_seconds");
        PlayerIdleTimeout           = new ServerSetting<int>    (client, "player_idle_timeout");
        AllowFlight                 = new ServerSetting<bool>   (client, "allow_flight");
        Motd                        = new ServerSetting<string> (client, "motd");
        SpawnProtectionRadius       = new ServerSetting<int>    (client, "spawn_protection_radius");
        ForceGameMode               = new ServerSetting<bool>   (client, "force_game_mode");
        GameMode                    = new ServerSetting<string> (client, "game_mode");
        ViewDistance                = new ServerSetting<int>    (client, "view_distance");
        SimulationDistance          = new ServerSetting<int>    (client, "simulation_distance");
        AcceptTransfers             = new ServerSetting<bool>   (client, "accept_transfers");
        StatusHeartbeatInterval     = new ServerSetting<int>    (client, "status_heartbeat_interval");
        OperatorUserPermissionLevel = new ServerSetting<int>    (client, "operator_user_permission_level");
        HideOnlinePlayers           = new ServerSetting<bool>   (client, "hide_online_players");
        StatusReplies               = new ServerSetting<bool>   (client, "status_replies");
        EntityBroadcastRange        = new ServerSetting<int>    (client, "entity_broadcast_range");
    }

    /// <summary>
    /// Controls automatic world saving on the server.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get whether automatic world saving is enabled on the server. Returns <c>enabled</c> (boolean).</para>
    /// <para><b>Set:</b> Enable or disable automatic world saving on the server. Parameter: <c>enable</c> (boolean). Returns <c>enabled</c> (boolean).</para>
    /// </remarks>
    public ServerSetting<bool> Autosave { get; }

    /// <summary>
    /// Controls the difficulty level of the server.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the current difficulty level of the server. Returns <c>difficulty</c> (string).</para>
    /// <para><b>Set:</b> Set the difficulty level of the server. Parameter: <c>difficulty</c> (string). Returns <c>difficulty</c> (string).</para>
    /// </remarks>
    public ServerSetting<string> Difficulty { get; }

    /// <summary>
    /// Controls whether allowlist enforcement is enabled (kicks players immediately when removed from allowlist).
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get whether allowlist enforcement is enabled (kicks players immediately when removed from allowlist). Returns <c>enforced</c> (boolean).</para>
    /// <para><b>Set:</b> Enable or disable allowlist enforcement (when enabled, players are kicked immediately upon removal from allowlist). Parameter: <c>enforce</c> (boolean). Returns <c>enforced</c> (boolean).</para>
    /// </remarks>
    public ServerSetting<bool> EnforceAllowlist { get; }

    /// <summary>
    /// Controls whether the allowlist is enabled on the server (controls whether only allowlisted players can join).
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get whether the allowlist is enabled on the server. Returns <c>used</c> (boolean).</para>
    /// <para><b>Set:</b> Enable or disable the allowlist on the server (controls whether only allowlisted players can join). Parameter: <c>use</c> (boolean). Returns <c>used</c> (boolean).</para>
    /// </remarks>
    public ServerSetting<bool> UseAllowlist { get; }

    /// <summary>
    /// Controls the maximum number of players allowed to connect to the server.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the maximum number of players allowed to connect to the server. Returns <c>max</c> (integer).</para>
    /// <para><b>Set:</b> Set the maximum number of players allowed to connect to the server. Parameter: <c>max</c> (integer). Returns <c>max</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> MaxPlayers { get; }

    /// <summary>
    /// Controls the number of seconds before the game is automatically paused when no players are online.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the number of seconds before the game is automatically paused when no players are online. Returns <c>seconds</c> (integer).</para>
    /// <para><b>Set:</b> Set the number of seconds before the game is automatically paused when no players are online. Parameter: <c>seconds</c> (integer). Returns <c>seconds</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> PauseWhenEmptySeconds { get; }

    /// <summary>
    /// Controls the number of seconds before idle players are automatically kicked from the server.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the number of seconds before idle players are automatically kicked from the server. Returns <c>seconds</c> (integer).</para>
    /// <para><b>Set:</b> Set the number of seconds before idle players are automatically kicked from the server. Parameter: <c>seconds</c> (integer). Returns <c>seconds</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> PlayerIdleTimeout { get; }

    /// <summary>
    /// Controls whether flight is allowed for players in Survival mode.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get whether flight is allowed for players in Survival mode. Returns <c>allowed</c> (boolean).</para>
    /// <para><b>Set:</b> Set whether flight is allowed for players in Survival mode. Parameter: <c>allowed</c> (boolean). Returns <c>allowed</c> (boolean).</para>
    /// </remarks>
    public ServerSetting<bool> AllowFlight { get; }

    /// <summary>
    /// Controls the server's message of the day displayed to players.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the server's message of the day displayed to players. Returns <c>message</c> (string).</para>
    /// <para><b>Set:</b> Set the server's message of the day displayed to players. Parameter: <c>message</c> (string). Returns <c>message</c> (string).</para>
    /// </remarks>
    public ServerSetting<string> Motd { get; }

    /// <summary>
    /// Controls the spawn protection radius in blocks (only operators can edit within this area).
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the spawn protection radius in blocks (only operators can edit within this area). Returns <c>radius</c> (integer).</para>
    /// <para><b>Set:</b> Set the spawn protection radius in blocks (only operators can edit within this area). Parameter: <c>radius</c> (integer). Returns <c>radius</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> SpawnProtectionRadius { get; }

    /// <summary>
    /// Controls whether players are forced to use the server's default game mode.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get whether players are forced to use the server's default game mode. Returns <c>forced</c> (boolean).</para>
    /// <para><b>Set:</b> Set whether players are forced to use the server's default game mode. Parameter: <c>force</c> (boolean). Returns <c>forced</c> (boolean).</para>
    /// </remarks>
    public ServerSetting<bool> ForceGameMode { get; }

    /// <summary>
    /// Controls the server's default game mode.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the server's default game mode. Returns <c>mode</c> (string).</para>
    /// <para><b>Set:</b> Set the server's default game mode. Parameter: <c>mode</c> (string). Returns <c>mode</c> (string).</para>
    /// </remarks>
    public ServerSetting<string> GameMode { get; }

    /// <summary>
    /// Controls the server's view distance in chunks.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the server's view distance in chunks. Returns <c>distance</c> (integer).</para>
    /// <para><b>Set:</b> Set the server's view distance in chunks. Parameter: <c>distance</c> (integer). Returns <c>distance</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> ViewDistance { get; }

    /// <summary>
    /// Controls the server's simulation distance in chunks.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the server's simulation distance in chunks. Returns <c>distance</c> (integer).</para>
    /// <para><b>Set:</b> Set the server's simulation distance in chunks. Parameter: <c>distance</c> (integer). Returns <c>distance</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> SimulationDistance { get; }

    /// <summary>
    /// Controls whether the server accepts player transfers from other servers.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get whether the server accepts player transfers from other servers. Returns <c>accepted</c> (boolean).</para>
    /// <para><b>Set:</b> Set whether the server accepts player transfers from other servers. Parameter: <c>accept</c> (boolean). Returns <c>accepted</c> (boolean).</para>
    /// </remarks>
    public ServerSetting<bool> AcceptTransfers { get; }

    /// <summary>
    /// Controls the interval in seconds between server status heartbeats.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the interval in seconds between server status heartbeats. Returns <c>seconds</c> (integer).</para>
    /// <para><b>Set:</b> Set the interval in seconds between server status heartbeats. Parameter: <c>seconds</c> (integer). Returns <c>seconds</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> StatusHeartbeatInterval { get; }

    /// <summary>
    /// Controls the permission level required for operator commands.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the permission level required for operator commands. Returns <c>level</c> (integer).</para>
    /// <para><b>Set:</b> Set the permission level required for operator commands. Parameter: <c>level</c> (integer). Returns <c>level</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> OperatorUserPermissionLevel { get; }

    /// <summary>
    /// Controls whether the server hides online player information from status queries.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get whether the server hides online player information from status queries. Returns <c>hidden</c> (boolean).</para>
    /// <para><b>Set:</b> Set whether the server hides online player information from status queries. Parameter: <c>hide</c> (boolean). Returns <c>hidden</c> (boolean).</para>
    /// </remarks>
    public ServerSetting<bool> HideOnlinePlayers { get; }

    /// <summary>
    /// Controls whether the server responds to connection status requests.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get whether the server responds to connection status requests. Returns <c>enabled</c> (boolean).</para>
    /// <para><b>Set:</b> Set whether the server responds to connection status requests. Parameter: <c>enable</c> (boolean). Returns <c>enabled</c> (boolean).</para>
    /// </remarks>
    public ServerSetting<bool> StatusReplies { get; }

    /// <summary>
    /// Controls the entity broadcast range as a percentage.
    /// </summary>
    /// <remarks>
    /// <para><b>Get:</b> Get the entity broadcast range as a percentage. Returns <c>percentage_points</c> (integer).</para>
    /// <para><b>Set:</b> Set the entity broadcast range as a percentage. Parameter: <c>percentage_points</c> (integer). Returns <c>percentage_points</c> (integer).</para>
    /// </remarks>
    public ServerSetting<int> EntityBroadcastRange { get; }
}