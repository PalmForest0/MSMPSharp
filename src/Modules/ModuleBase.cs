using MSMPSharp.Core;

namespace MSMPSharp.Modules;

public abstract class ModuleBase
{
    protected readonly MsmpClient _client;

    private protected ModuleBase(MsmpClient client)
    {
        _client = client;
    }

    internal virtual void RegisterNotificationHandlers() { }
}