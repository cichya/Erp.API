using Erp.API.Models;
using Erp.API.XmlDataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.API.Data
{
	public class DataContext : DbContext
	{
		private readonly IDataStore<EmployeeModel> dataStore;

		public DbSet<EmployeeModel> Employees { get; set; }

		public DataContext(DbContextOptions<DataContext> options, IDataStore<EmployeeModel> dataStore)
			: base(options)
		{
			this.dataStore = dataStore;
		}

		public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
		{
			if (this.Employees.Count() > 0)
			{
				(entity as EmployeeModel).Id = this.Employees.Select(x => x.Id).Max() + 1;
			}

			var added = base.Add(entity);

			this.dataStore.Add(added.Entity as EmployeeModel);

			return added;
		}

		public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
		{
			var added = base.Remove(entity);

			this.dataStore.Remove(added.Entity as EmployeeModel);

			return added;
		}

		public EntityEntry AddWhenInit(EmployeeModel entity)
		{
			return base.Add(entity);
		}

		public void Update(EmployeeModel entity)
		{
			this.dataStore.Update(entity);
		}

		public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var result = await base.SaveChangesAsync(cancellationToken);

			if (result > 0)
			{
				this.dataStore.Save();
			}

			return result;
		}
	}
}
