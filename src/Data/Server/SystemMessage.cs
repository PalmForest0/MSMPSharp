using MSMPSharp.Data.Game;

namespace MSMPSharp.Data.Server;

public class SystemMessage
{
    public Player[] ReceivingPlayers { get; set; }
    public bool Overlay { get; set; }
    public Message Message { get; set; }

    public SystemMessage(Message message, Player[] receivingPlayers, bool overlay)
    {
        Message = message;
        ReceivingPlayers = receivingPlayers;
        Overlay = overlay;
    }
}