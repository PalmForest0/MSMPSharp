namespace MSMPSharp.Extensions;

internal static class DateTimeExtensions
{
    internal static string ToMCString(this DateTime dateTime) => dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
}