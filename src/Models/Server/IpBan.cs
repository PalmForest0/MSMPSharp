using MSMPSharp.Extensions;

namespace MSMPSharp.Models.Server;

public sealed class IpBan
{
    public string Ip { get; }
    public string? Reason { get; }
    public string? Expires { get; }
    public string? Source { get; }

    /// <summary>
    /// Defines the data of an ip ban that applies to a specific Ip address.
    /// </summary>
    /// <param name="ip">The Ip address this ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="expires">Optional expiry DateTime of this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    public IpBan(string ip, string? reason = null, DateTime? expires = null, string ? source = null)
    {
        Ip = ip;
        Reason = reason;
        Expires = expires?.ToMCString();
        Source = source;
    }
}