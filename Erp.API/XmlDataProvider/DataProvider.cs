using Erp.API.Data;
using Erp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.XmlDataProvider
{
	public class DataProvider
	{
		public static void Init(IServiceProvider serviceProvider)
		{
			var dataStore = serviceProvider.GetRequiredService(typeof(IDataStore<EmployeeModel>)) as IDataStore<EmployeeModel>;

			using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>(), dataStore))
			{
				if (context.Employees.Any())
				{
					return;
				}

				foreach (var entity in dataStore.Get())
				{
					context.AddWhenInit(entity);
				}

				context.SaveChanges();
			}
		}
	}
}
