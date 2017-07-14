namespace ATag.EntityFrameworkCore
{
    using System;
    using System.Linq;
    using ATag.Core;
    using LinqKit;
    using Microsoft.EntityFrameworkCore;

    internal static class Extensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,
            DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }

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

        public static PagedEntity<T> WithPaging<T>(this IQueryable<T> query, int pageIndex, int pageSize)
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
                Results = query.Skip(excludedRows).Take(Math.Min(pageSize, rowsCount)).ToArray(),
                TotalCount = rowsCount
            };
        }
    }
}