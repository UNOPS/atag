namespace ATag.EntityFrameworkCore;

using ATag.Core;
using LinqKit;
using Microsoft.EntityFrameworkCore;

internal static class LoadTagsExtensions
{
    public static IQueryable<Tag> BelongingTo(this IQueryable<Tag> queryable, params TagOwnerFilter[] filters)
    {
        var filter = PredicateBuilder.New<Tag>(true);

        foreach (var recipient in filters)
        {
            filter = filter.Or(t => t.OwnerType == recipient.OwnerType && t.OwnerId == recipient.OwnerId);
        }

        return queryable
            .AsExpandable()
            .Where(filter);
    }

    internal static IQueryable<TaggedEntity> FilterTaggedEntities(this IQueryable<TaggedEntity> queryable,
        ICollection<int> tagIds,
        string entityType,
        string entityKey)
    {
        return queryable.Where(t => t.EntityType == entityType && t.EntityKey == entityKey && tagIds.Contains(t.TagId));
    }

    internal static IQueryable<TaggedEntity> FilterTaggedEntities(this IQueryable<TaggedEntity> queryable,
        int tagId,
        string entityType,
        string entityKey)
    {
        return queryable.Where(a => a.TagId == tagId && a.EntityType == entityType && a.EntityKey == entityKey);
    }

    internal static PagedEntity<T> WithPaging<T>(this IQueryable<T> query, int pageIndex, int pageSize)
    {
        if (pageSize <= 0)
        {
            pageSize = 10;
        }

        //Total result count
        var rowsCount = query.Count();

        //If page number should be > 0 else set to first page
        if (rowsCount <= pageSize || pageIndex <= 0)
        {
            pageIndex = 1;
        }

        //Calculate number of rows to skip on page size
        var excludedRows = (pageIndex - 1) * pageSize;

        return new PagedEntity<T>
        {
            Results = query.Skip(excludedRows).Take(pageSize).ToArray(),
            TotalCount = rowsCount
        };
    }

    internal static async Task<PagedEntity<T>> WithPagingAsync<T>(
        this IQueryable<T> query,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (pageSize <= 0)
        {
            pageSize = 10;
        }

        // Total result count
        var rowsCount = await query.CountAsync(cancellationToken);

        // If page number should be > 0 else set to first page
        if (rowsCount <= pageSize || pageIndex <= 0)
        {
            pageIndex = 1;
        }

        // Calculate number of rows to skip on page size
        var excludedRows = (pageIndex - 1) * pageSize;

        return new PagedEntity<T>
        {
            Results = await query.Skip(excludedRows).Take(pageSize).ToListAsync(cancellationToken),
            TotalCount = rowsCount
        };
    }
}