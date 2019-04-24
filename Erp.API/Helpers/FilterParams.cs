using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Helpers
{
	public class FilterParams
	{
		private const int MaxPageSize = 50;
		public int PageNumber { get; set; } = 1;
		private int pageSize = 10;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
		}

		public string LastNameFilter { get; set; }
		public string TaxNumberFilter { get; set; }
		public string WorkingPositionFilter { get; set; }
	}
}
