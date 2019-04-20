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
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			
		}
	}
}
