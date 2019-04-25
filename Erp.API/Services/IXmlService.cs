using Erp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.API.Services
{
	public interface IXmlService
	{
		IList<EmployeeModel> LoadXml(string filePath);
		void SaveXml(string filePath, IList<EmployeeModel> employees);
	}
}
