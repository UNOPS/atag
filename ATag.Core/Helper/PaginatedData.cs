namespace ATag.Core.Helper
{
    using System.Collections.Generic;

    public class PaginatedData<T>
	{
		public IEnumerable<T> Results { get; set; }
		public int TotalCount { get; set; }
	}
}