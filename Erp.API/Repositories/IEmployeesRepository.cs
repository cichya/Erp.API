using Erp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Repositories
{
	interface IEmployeesRepository
	{
		void Add(EmployeeModel entity);
		void Delete(EmployeeModel entity);
		void Update(EmployeeModel entity);
		Task<EmployeeModel> Get(int id);
		Task<List<EmployeeModel>> Get();
		Task<bool> Save();
	}
}
