using System.Text.Json.Serialization;

namespace MSMPSharp.Models.Server;

public sealed class Version
{
    public string Name { get; }
    public string Protocol { get; }

    [JsonConstructor]
    private Version(string name, string protocol)
    {
        Name = name;
        Protocol = protocol;
    }
}