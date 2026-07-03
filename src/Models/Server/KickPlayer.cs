using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public sealed class KickPlayer
{
    public Player Player { get; }
    public Message Message { get; }

    /// <summary>
    /// Creates data to kick a provided player with the specified message.
    /// </summary>
    /// <param name="player">Player to kick from the server.</param>
    /// <param name="message">Message to show to the kicked player. If none is provided, defaults to "You have been kicked via the Server Management Protocol."</param>
    public KickPlayer(Player player, Message? message = null)
    {
        Player = player;
        Message = message ?? "You have been kicked via the Server Management Protocol.";
    }
}