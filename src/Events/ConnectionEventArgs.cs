namespace MSMPSharp.Events;

public class ConnectionEventArgs : EventArgs
{
    public Uri ServerUri { get; }

    public ConnectionEventArgs(Uri serverUri) => ServerUri = serverUri;
}