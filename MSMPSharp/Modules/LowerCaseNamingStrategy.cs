using Newtonsoft.Json.Serialization;

namespace MSMPSharp.Modules;

public class LowerCaseNamingStrategy : NamingStrategy
{
    protected override string ResolvePropertyName(string name) => name.ToLowerInvariant();
}