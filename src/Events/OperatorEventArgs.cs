using MSMPSharp.Models.Server;

namespace MSMPSharp.Events;

public class OperatorEventArgs(Operator op) : EventArgs
{
    public Operator Operator { get; } = op;
}
