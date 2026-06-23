namespace MSMPSharp.Events;

public class ConnectionEventArgs(Uri serverUri) : EventArgs
{
    public Uri ServerUri { get; } = serverUri;
}