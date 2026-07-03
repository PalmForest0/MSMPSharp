namespace MSMPSharp.Extensions;

internal static class DateTimeExtensions
{
    internal static string ToMCString(this DateTime dateTime)
    {
        var utc = dateTime.Kind == DateTimeKind.Unspecified
            ? throw new ArgumentException("DateTime must have a specified kind (UTC or Local).", nameof(dateTime))
            : dateTime.ToUniversalTime();

        return utc.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}