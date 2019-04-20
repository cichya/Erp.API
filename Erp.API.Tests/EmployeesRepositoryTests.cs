using Erp.API.Data;
using Erp.API.Models;
using Erp.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
				FirstName = "New", LastName = "Employee", Birth = DateTime.Now.AddYears(-10), Salary = 10000, TaxNumber = 0987, WorkingPosition = "Junior Dev"
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
		public async Task Delete_Test()
		{
			var target = new EmployeesRepository(this.dataContext);

			var empToRemove = await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == 1);

			target.Delete(empToRemove);

			await target.Save();

			Assert.IsFalse(await this.dataContext.Employees.AnyAsync(x => x.Id == 1));
		}

		[TestMethod]
		public async Task Update_Test()
		{
			var target = new EmployeesRepository(this.dataContext);

			var empToUpdate = await this.dataContext.Employees.FirstOrDefaultAsync(x => x.Id == 2);

			empToUpdate.FirstName = "xxx";

			target.Update(empToUpdate);

			await target.Save();

			Assert.IsTrue(await this.dataContext.Employees.AnyAsync(x => x.Id == empToUpdate.Id && x.FirstName == empToUpdate.FirstName));
		}

		[TestMethod]
		public async Task Get_By_Id_Test()
		{
			var target = new EmployeesRepository(this.dataContext);

			var employee = await target.Get(2);

			Assert.IsNotNull(employee);
			Assert.AreEqual(2, employee.Id);
		}

		[TestMethod]
		public async Task Get_All_Test()
		{
			var target = new EmployeesRepository(this.dataContext);

			var employees = await target.Get();

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
					Birth = DateTime.Now.AddYears(-3), FirstName = "George", LastName = "Kovalsky", Salary = 1000, TaxNumber = 1234, WorkingPosition = "CEO"
				},
				new EmployeeModel()
				{
					Birth = DateTime.Now.AddYears(-5), FirstName = "John", LastName = "Doe", Salary = 2000, TaxNumber = 4567, WorkingPosition = "Developer"
				}
			};

			var options = new DbContextOptionsBuilder<DataContext>()
				.UseInMemoryDatabase(databaseName: "EmployeeListDatabase")
				.Options;

			this.dataContext = new DataContext(options);


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
