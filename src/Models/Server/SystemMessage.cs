using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public class SystemMessage
{
    public Player[] ReceivingPlayers { get; set; }
    public bool Overlay { get; set; }
    public Message Message { get; set; }
}