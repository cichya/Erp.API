using Erp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.XmlDataProvider
{
	public interface IDataStore<T>
	{
		IQueryable<T> Get();
		void Add(T entity);
		void Update(T entity);
		void Remove(T employeeModel);
		void Save();
	}
}
