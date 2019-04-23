using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Helpers
{
	public class PagedDataList<T> : List<T>
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }

		public PagedDataList(List<T> items, int count, int pageNumber, int pageSize)
		{
			this.TotalCount = count;
			this.PageSize = pageSize;
			this.CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			this.AddRange(items);
		}

		public static async Task<PagedDataList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
		{
			var count = await source.CountAsync();
			var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
			return new PagedDataList<T>(items, count, pageNumber, pageSize);
		}
	}
}
