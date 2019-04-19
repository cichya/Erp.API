using System;

namespace Erp.API.Models
{
	public class EmployeeModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime Birth { get; set; }
		public string WorkingPosition { get; set; }
		public decimal Salary { get; set; }
		public int TaxNumber { get; set; }
	}
}