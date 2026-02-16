namespace MSMPSharp.Extensions;

internal static class DateTimeExtensions
{
    extension(DateTime dateTime)
    {
        public string ToMCString() => dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}