using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.DTOs
{
	public class EmployeeForListDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public string WorkingPosition { get; set; }
		public decimal Salary { get; set; }
		public string TaxNumber { get; set; }
	}
}
