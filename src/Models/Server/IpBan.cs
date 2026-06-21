using MSMPSharp.Extensions;

namespace MSMPSharp.Models.Server;

public class IpBan
{
    public string Ip { get; set; }
    public string? Reason { get; set; }
    public string? Expires { get; set; }
    public string? Source { get; set; }

    /// <summary>
    /// Defines the data of an ip ban that applies to a specific Ip address.
    /// </summary>
    /// <param name="ip">The Ip address this ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    /// <param name="expires">Optional expiry DateTime of this ip ban.</param>
    public IpBan(string ip, DateTime? expires = null, string? reason = null, string? source = null)
    {
        Ip = ip;
        Expires = expires?.ToMCString();

        Reason = reason;
        Source = source;
    }
}