using MSMPSharp.Models.Server;

namespace MSMPSharp.Events;

public sealed class OperatorEventArgs : EventArgs
{
    public Operator Operator { get; }

    internal OperatorEventArgs(Operator op)
    {
        Operator = op;
    }
}
