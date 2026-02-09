using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

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

    /// <summary>
    /// Creates data to kick a provided player with a message using the specified string literal.
    /// </summary>
    /// <param name="player">Player to kick from the server.</param>
    /// <param name="messageString">String literal to use for the message that will be shown to the player.</param>
    public KickPlayer(Player player, string messageString)
    {
        Player = player;
        Message = new Message(messageString);
    }
}