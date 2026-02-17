using MSMPSharp.Data.Game;

namespace MSMPSharp.Data.Server;

public class KickPlayer
{
    public Player Player { get; set; }
    public Message Message { get; set; }

    /// <summary>
    /// Creates data to kick a provided player with the specified message.
    /// </summary>
    /// <param name="player">Player to kick from the server.</param>
    /// <param name="message">Message to show to the kicked player.</param>
    public KickPlayer(Player player, Message message)
    {
        Player = player;
        Message = message;
    }
}