namespace ATag.Core
{
    using System.Collections.Generic;

    public class PagedEntity<T>
	{
		public IEnumerable<T> Results { get; set; }
		public int TotalCount { get; set; }
	}
}