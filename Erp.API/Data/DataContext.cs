using Erp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Data
{
	public class DataContext : DbContext
	{
		public DbSet<EmployeeModel> Employees { get; set; }

		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
			List<EmployeeModel> tmp = new List<EmployeeModel>()
			{
				new EmployeeModel
				{
					Id = 1, Birth = DateTime.Now, FirstName = "Jan", LastName = "Kowalski", Salary = 100, TaxNumber = 11, WorkingPosition = "Programista"
				}
			};

			foreach (var item in tmp)
			{
				this.Employees.Add(item);
			}

			this.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			
		}
	}
}
