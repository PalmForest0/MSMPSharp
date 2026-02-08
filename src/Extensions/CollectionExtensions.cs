namespace MSMPSharp.Extensions;

internal static class CollectionExtensions
{
    extension<T>(ICollection<T> collection)
    {
        public bool IsNullOrEmpty() => collection is null || collection.Count == 0;
    }
}