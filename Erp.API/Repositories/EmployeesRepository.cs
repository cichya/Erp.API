using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.API.Data;
using Erp.API.Helpers;
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

		public async Task<PagedDataList<EmployeeModel>> Get(FilterParams filterParams)
		{
			var employees = this.dataContext.Employees.AsQueryable();

			if (!string.IsNullOrEmpty(filterParams.LastNameFilter))
			{
				employees = employees.Where(x => x.LastName.ToLower() == filterParams.LastNameFilter.ToLower());
			}

			if (!string.IsNullOrEmpty(filterParams.TaxNumberFilter))
			{
				employees = employees.Where(x => x.TaxNumber.ToString() == filterParams.TaxNumberFilter);
			}

			if (!string.IsNullOrEmpty(filterParams.WorkingPositionFilter))
			{
				employees = employees.Where(x => x.WorkingPosition.ToLower() == filterParams.WorkingPositionFilter.ToLower());
			}

			return await PagedDataList<EmployeeModel>.CreateAsync(employees, filterParams.PageNumber, filterParams.PageSize);
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
