using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public class KickPlayer
{
    public Player Player { get; set; }
    public Message Message { get; set; }
}