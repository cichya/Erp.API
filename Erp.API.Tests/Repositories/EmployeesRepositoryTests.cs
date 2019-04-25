using Erp.API.Data;
using Erp.API.Helpers;
using Erp.API.Models;
using Erp.API.Repositories;
using Erp.API.XmlDataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Erp.API.Tests
{
	[TestClass]
	public class EmployeesRepositoryTests
	{
		private DataContext dataContext;

		[TestMethod]
		public async Task Add_Test()
		{
			var target = new EmployeesRepository(this.dataContext);

			var newEmp = new EmployeeModel
			{
				FirstName = "New", LastName = "Employee", Birth = DateTime.Now.AddYears(-10), Salary = 10000, TaxNumber = "0987", WorkingPosition = "Junior Dev"
			};

			target.Add(newEmp);

			await target.Save();

			Assert.AreEqual(3, newEmp.Id);
			Assert.IsTrue(await this.dataContext.Employees.AnyAsync(x => x.Id == newEmp.Id));
			Assert.IsTrue(await this.dataContext.Employees.AnyAsync(x => x.LastName == newEmp.LastName));
			Assert.IsTrue(await this.dataContext.Employees.AnyAsync(x => x.FirstName == newEmp.FirstName));
			Assert.IsTrue(await this.dataContext.Employees.AnyAsync(x => x.Birth == newEmp.Birth));
			Assert.IsTrue(await this.dataContext.Employees.AnyAsync(x => x.Salary == newEmp.Salary));
			Assert.IsTrue(await this.dataContext.Employees.AnyAsync(x => x.TaxNumber == newEmp.TaxNumber));
			Assert.IsTrue(await this.dataContext.Employees.AnyAsync(x => x.WorkingPosition == newEmp.WorkingPosition));
		}

		[TestMethod]
		public async Task Get_All_Test()
		{
			var target = new EmployeesRepository(this.dataContext);

			var employees = await target.Get(new FilterParams());

			Assert.IsNotNull(employees);
			Assert.AreNotEqual(0, employees.Count);
		}

		[TestInitialize]
		public void Init()
		{
			List<EmployeeModel> employees = new List<EmployeeModel>()
			{
				new EmployeeModel()
				{
					Birth = DateTime.Now.AddYears(-3), FirstName = "George", LastName = "Kovalsky", Salary = 1000, TaxNumber = "1234", WorkingPosition = "CEO"
				},
				new EmployeeModel()
				{
					Birth = DateTime.Now.AddYears(-5), FirstName = "John", LastName = "Doe", Salary = 2000, TaxNumber = "4567", WorkingPosition = "Developer"
				}
			};

			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
				.Options;

			var dataStoreMock = new Mock<IDataStore<EmployeeModel>>(MockBehavior.Strict);

			dataStoreMock.Setup(m => m.Add(It.IsAny<EmployeeModel>()));
			dataStoreMock.Setup(m => m.Remove(It.IsAny<EmployeeModel>()));
			dataStoreMock.Setup(m => m.Update(It.IsAny<EmployeeModel>()));
			dataStoreMock.Setup(m => m.Save());

			this.dataContext = new DataContext(options, dataStoreMock.Object);

			foreach (var emp in employees)
			{
				dataContext.Add(emp);
			}

			this.dataContext.SaveChanges();
		}

		[TestCleanup]
		public void CleanUp()
		{
			this.dataContext.Dispose();
		}
	}
}
