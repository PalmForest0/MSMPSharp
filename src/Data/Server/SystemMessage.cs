using MSMPSharp.Data.Game;

namespace MSMPSharp.Data.Server;

public class SystemMessage
{
    public Player[] ReceivingPlayers { get; set; }
    public bool Overlay { get; set; }
    public Message Message { get; set; }

    public SystemMessage(Player[] players, Message message, bool overlay = false)
    {
        ReceivingPlayers = players;
        Message = message;
        Overlay = overlay;
    }

    public SystemMessage(Player[] players, string messageLiteral, bool overlay = false)
    {
        ReceivingPlayers = players;
        Message = new Message(messageLiteral);
        Overlay = overlay;
    }
}