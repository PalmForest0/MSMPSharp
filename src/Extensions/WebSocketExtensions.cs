using System.Net.WebSockets;
using System.Text;

namespace MSMPSharp.Extensions;

internal static class WebSocketExtensions
{
    /// <summary>
    /// Receives a message from the WebSocket in chunks and returns it as a string.
    /// </summary>
    /// <param name="socket">Open ClientWebSocket instance</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The received message as a string</returns>
    /// <exception cref="WebSocketException"/>
    internal static async Task<string> ReceiveInChunksAsync(this ClientWebSocket socket, CancellationToken cancellationToken)
    {
        var buffer = new byte[4096];
        using var ms = new MemoryStream();

        WebSocketReceiveResult result;

        do
        {
            result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                var ex = new WebSocketException(
                    $"WebSocket closed unexpectedly while receiving a message (status: {result.CloseStatus}, description: \"{result.CloseStatusDescription}\").");

                ex.Data["CloseStatus"] = result.CloseStatus;
                ex.Data["CloseStatusDescription"] = result.CloseStatusDescription;
                ex.Data["SocketState"] = socket.State;
                ex.Data["BytesReceivedBeforeClose"] = ms.Length;

                throw ex;
            }

            await ms.WriteAsync(buffer.AsMemory(0, result.Count), cancellationToken);
        }
        while (!result.EndOfMessage);

        return Encoding.UTF8.GetString(ms.ToArray());
    }
}
