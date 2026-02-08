using Newtonsoft.Json.Serialization;

namespace MSMPSharp.Extensions;

public class LowerCaseNamingStrategy : NamingStrategy
{
    protected override string ResolvePropertyName(string name) => name.ToLowerInvariant();
}