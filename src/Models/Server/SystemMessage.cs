using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public sealed class SystemMessage
{
    public Message Message { get; }
    public Player[] ReceivingPlayers { get; }
    public bool Overlay { get; }

    private SystemMessage(Message message, Player[] players, bool overlay)
    {
        Message = message;
        ReceivingPlayers = players;
        Overlay = overlay;
    }

    /// <summary>
    /// Creates a system message for the specified players which will be displayed in chat.
    /// </summary>
    /// <param name="message">Message to be displayed</param>
    /// <param name="players">Players to receive the message</param>
    /// <returns>The system message with the provided parameters</returns>
    public static SystemMessage InChat(Message message, params Player[] players) => new SystemMessage(message, players, false);

    /// <summary>
    /// Creates a system message for the specified players which will be displayed in the overlay (above the players' hotbar).
    /// </summary>
    /// <param name="message">Message to be displayed</param>
    /// <param name="players">Players to receive the message</param>
    /// <returns>The system message with the provided parameters</returns>
    public static SystemMessage InOverlay(Message message, params Player[] players) => new SystemMessage(message, players, true);
}