namespace MSMPSharp.Events;

public sealed class ConnectionEventArgs : EventArgs
{
    public Uri ServerUri { get; }

    internal ConnectionEventArgs(Uri serverUri)
    {
        ServerUri = serverUri;
    }
}