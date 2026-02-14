namespace MSMPSharp.Data.Server;

public class IpBan
{
    public string Ip { get; set; } = "";
    public string Reason { get; set; } = "";
    public string Expires { get; set; } = "";
    public string Source { get; set; } = "";

    /// <summary>
    /// Defines the data of an ip ban that applies to a specific Ip address.
    /// </summary>
    /// <param name="ip">The Ip address this ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    /// <param name="expires">Optional expiry of this ip ban.</param>
    public IpBan(string ip, string reason = "", string source = "", string expires = "")
    {
        Ip = ip;
        Reason = reason;
        Source = source;
        Expires = expires;
    }
}