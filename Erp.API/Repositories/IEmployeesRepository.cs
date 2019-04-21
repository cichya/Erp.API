using Erp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Repositories
{
	public interface IEmployeesRepository
	{
		void Add(EmployeeModel entity);
		void Delete(EmployeeModel entity);
		void Update(EmployeeModel oldEntity, EmployeeModel newEntity);
		Task<EmployeeModel> Get(int id);
		Task<List<EmployeeModel>> Get();
		Task<bool> Save();
	}
}
