using MSMPSharp.Core;

namespace MSMPSharp.Modules;

public abstract class ModuleBase
{
    protected readonly MsmpClient client;

    private protected ModuleBase(MsmpClient client)
    {
        this.client = client;
    }
}