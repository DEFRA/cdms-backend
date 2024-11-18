namespace Cdms.Backend.Data.Extensions;

public static class QueryableExtensions
{
    public static async Task<List<TSource>> ToListAsync<TSource>(
        this IQueryable<TSource> source,
        CancellationToken cancellationToken = default)
    {
        if (source is IAsyncEnumerable<TSource> asyncEnumerable)
        {
            var list = new List<TSource>();
            await foreach (var element in asyncEnumerable.WithCancellation(cancellationToken))
            {
                list.Add(element);
            }

            return list;
        }


        return source.ToList();

    }
}