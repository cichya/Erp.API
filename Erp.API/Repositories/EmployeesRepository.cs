using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.API.Data;
using Erp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Erp.API.Repositories
{
	public class EmployeesRepository : IEmployeesRepository
	{
		private readonly DataContext dataContext;

		public EmployeesRepository(DataContext dataContext)
		{
			this.dataContext = dataContext;
		}

		public void Add(EmployeeModel entity)
		{
			this.dataContext.Add(entity);
		}

		public void Delete(EmployeeModel entity)
		{
			this.dataContext.Remove(entity);
		}

		public async Task<EmployeeModel> Get(int id)
		{
			return await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<EmployeeModel>> Get()
		{
			return await this.dataContext.Employees.ToListAsync();
		}

		public async Task<bool> Save()
		{
			return await this.dataContext.SaveChangesAsync() > 0;
		}

		public void Update(EmployeeModel oldEntity, EmployeeModel newEntity)
		{
			this.dataContext.Entry(oldEntity).State = EntityState.Detached;
			this.dataContext.Entry(newEntity).State = EntityState.Modified;
		}
	}
}
