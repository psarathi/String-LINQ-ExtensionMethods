namespace LINQExtensions;

/// <summary>
/// Extension methods for LINQ operations
/// </summary>
public static class LinqExtension
{
    /// <summary>
    /// Skips the given number of items starting from the end of a collection
    /// </summary>
    /// <typeparam name="T">The type of the collection</typeparam>
    /// <param name="source">The collections of items</param>
    /// <param name="numberOfItemsToSkip">The number of items to skip from the collection</param>
    /// <returns>A collection of items with specified number of items skipped starting from the end</returns>
    public static IEnumerable<T>? SkipLast<T>(this IEnumerable<T>? source, int numberOfItemsToSkip)
    {
        if (source is null)
            return null;

        var items = source as IList<T> ?? source.ToList();

        if (items.Count == 0)
            return null;

        var itemsToSkip = Math.Abs(numberOfItemsToSkip);

        if (itemsToSkip >= items.Count)
            return null;

        return numberOfItemsToSkip > 0
            ? source.Take(items.Count - itemsToSkip)
            : source.Skip(itemsToSkip);
    }

    /// <summary>
    /// Gets the given number of items starting from the end of a collection
    /// </summary>
    /// <typeparam name="T">The type of the collection</typeparam>
    /// <param name="source">The collections of items</param>
    /// <param name="numberOfItemsToTake">The number of items to take from the collection</param>
    /// <returns>A collection of items with specified number of items taken starting from the end</returns>
    public static IEnumerable<T>? TakeLast<T>(this IEnumerable<T>? source, int numberOfItemsToTake)
    {
        if (source is null)
            return null;

        var items = source as IList<T> ?? source.ToList();

        if (items.Count == 0)
            return null;

        var itemsToTake = Math.Abs(numberOfItemsToTake);

        if (itemsToTake >= items.Count)
            return source;

        return numberOfItemsToTake > 0
            ? source.Skip(items.Count - itemsToTake)
            : source.Take(itemsToTake);
    }

    /// <summary>
    /// Gets the items from the collection whose indices are provided
    /// </summary>
    /// <typeparam name="T">The type of the collection</typeparam>
    /// <param name="source">The collection of items</param>
    /// <param name="indexOfItemsToTake">A collection of indices of the items to take</param>
    /// <returns>A collection of items whose indices were provided</returns>
    public static IEnumerable<T>? TakeNs<T>(this IEnumerable<T>? source, params ReadOnlySpan<int> indexOfItemsToTake)
    {
        if (source is null)
            return null;

        var sourceList = source.ToList();

        if (sourceList.Count == 0)
            return null;

        if (indexOfItemsToTake.Length == 0)
            return null;

        return sourceList.Where((_, i) => indexOfItemsToTake.Contains(i));
    }

    /// <summary>
    /// Skips the items from the collection whose indices are provided
    /// </summary>
    /// <typeparam name="T">The type of the collection</typeparam>
    /// <param name="source">The collection of items</param>
    /// <param name="indexOfItemsToSkip">A collection of indices of the items to skip</param>
    /// <returns>A collection of items without the skipped ones</returns>
    public static IEnumerable<T>? SkipNs<T>(this IEnumerable<T>? source, params ReadOnlySpan<int> indexOfItemsToSkip)
    {
        if (source is null)
            return null;

        var sourceList = source.ToList();

        if (sourceList.Count == 0)
            return null;

        if (indexOfItemsToSkip.Length == 0)
            return source;

        return sourceList.Where((_, i) => !indexOfItemsToSkip.Contains(i));
    }
}
