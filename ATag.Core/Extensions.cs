namespace ATag.Core
{
    using System;
    using System.Linq;

    internal static class Extensions
    {
        public static void EnforceMaxLength(this string value, int maxLength)
        {
            if (value?.Length > maxLength)
            {
                throw new TagException($"String exceeds max length of {maxLength}.");
            }
        }

        public static PagedEntity<T> WithPaging<T>(
            this IQueryable<T> query,
            int pageNum,
            int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            //Total result count
            var rowsCount = query.Count();

            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNum <= 0)
            {
                pageNum = 1;
            }

            //Calculate number of rows to skip on page size
            var excludedRows = (pageNum - 1) * pageSize;

            return new PagedEntity<T>
            {
                Results = query.Skip(excludedRows).Take(Math.Min(pageSize, rowsCount)).ToArray(),
                TotalCount = rowsCount
            };
        }
    }
}