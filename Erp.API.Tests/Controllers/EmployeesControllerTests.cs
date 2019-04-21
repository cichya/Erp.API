using AutoMapper;
using Erp.API.Controllers;
using Erp.API.DTOs;
using Erp.API.Models;
using Erp.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Erp.API.Tests.Controllers
{
	[TestClass]
	public class EmployeesControllerTests
	{
		private Mock<IEmployeesRepository> employeesRepository;
		private Mock<IMapper> mapper;

		[TestMethod]
		public async Task Get_All_Test()
		{
			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.GetEmployees();

			var okObjectResult = result as OkObjectResult;

			Assert.IsNotNull(okObjectResult);

			var employees = okObjectResult.Value as List<EmployeeForListDto>;

			Assert.IsNotNull(employees);

			Assert.AreEqual(2, employees.Count);

			Assert.IsNotNull(employees.Find(x => x.Id == 1));
			Assert.IsNotNull(employees.Find(x => x.Id == 2));

			this.employeesRepository.Verify(m => m.Get(), Times.Once);
		}

		[TestMethod]
		public async Task Get_By_Id_Employee_Null_Return_NotFound_Test()
		{
			EmployeeModel tmp = null;

			this.employeesRepository.Setup(m => m.Get(1)).Returns(Task.FromResult(tmp));

			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.GetEmployee(1);

			var notFoundResult = result as NotFoundResult;

			Assert.IsNotNull(notFoundResult);

			this.employeesRepository.Verify(m => m.Get(1), Times.Once);
		}

		[TestMethod]
		public async Task Get_By_Id_Employee_Return_Employee_Test()
		{
			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.GetEmployee(1);

			var okObjectResult = result as OkObjectResult;

			var employee = okObjectResult.Value as EmployeeForListDto;

			Assert.IsNotNull(employee);

			Assert.AreEqual(1, employee.Id);


			this.employeesRepository.Verify(m => m.Get(1), Times.Once);
		}

		[TestMethod]
		public async Task Update_Employee_Ids_Not_Equal_Return_BadRequest_Test()
		{
			var tmp = new EmployeeModel
			{
				Id = 2
			};

			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.UpdateEmployee(1, tmp);

			var badRequestResult = result as BadRequestResult;

			Assert.IsNotNull(badRequestResult);

			this.employeesRepository.Verify(m => m.Update(It.IsAny<EmployeeModel>(), It.IsAny<EmployeeModel>()), Times.Never);
			this.employeesRepository.Verify(m => m.Save(), Times.Never);
		}

		[TestMethod]
		public async Task Update_Employee_Get_Returned_Null_Return_NotFound_Test()
		{
			var tmp = new EmployeeModel
			{
				Id = 1
			};

			EmployeeModel tmp2 = null;

			this.employeesRepository.Setup(m => m.Get(1)).Returns(Task.FromResult(tmp2));

			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.UpdateEmployee(1, tmp);

			var notFoundResult = result as NotFoundResult;

			Assert.IsNotNull(notFoundResult);

			this.employeesRepository.Verify(m => m.Update(It.IsAny<EmployeeModel>(), It.IsAny<EmployeeModel>()), Times.Never);
			this.employeesRepository.Verify(m => m.Save(), Times.Never);
		}

		[TestMethod]
		public async Task Update_Employee_Save_Is_False_Throw_Exception_Test()
		{
			try
			{
				var tmp = new EmployeeModel
				{
					Id = 1
				};

				this.employeesRepository.Setup(m => m.Save()).Returns(Task.FromResult(false));

				EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

				IActionResult result = await target.UpdateEmployee(1, tmp);

				Assert.Fail("Should be an exception");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Failed to update employee 1", ex.Message);

				this.employeesRepository.Verify(m => m.Update(It.IsAny<EmployeeModel>(), It.IsAny<EmployeeModel>()), Times.Once);
				this.employeesRepository.Verify(m => m.Save(), Times.Once);
			}
		}

		[TestMethod]
		public async Task Update_Employee_Return_NoContent_Test()
		{
			var tmp = new EmployeeModel
			{
				Id = 1
			};

			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.UpdateEmployee(1, tmp);

			var noContentResult = result as NoContentResult;

			Assert.IsNotNull(noContentResult);

			this.employeesRepository.Verify(m => m.Update(It.IsAny<EmployeeModel>(), It.IsAny<EmployeeModel>()), Times.Once);
			this.employeesRepository.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public async Task Delete_Employee_Get_Returned_Null_Return_NotFound_Test()
		{
			EmployeeModel tmp2 = null;

			this.employeesRepository.Setup(m => m.Get(1)).Returns(Task.FromResult(tmp2));

			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.Delete(1);

			var notFoundResult = result as NotFoundResult;

			Assert.IsNotNull(notFoundResult);

			this.employeesRepository.Verify(m => m.Delete(It.IsAny<EmployeeModel>()), Times.Never);
			this.employeesRepository.Verify(m => m.Save(), Times.Never);
		}

		[TestMethod]
		public async Task Delete_Employee_Return_NoContent_Test()
		{
			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.Delete(1);

			var noContentResult = result as NoContentResult;

			Assert.IsNotNull(noContentResult);

			this.employeesRepository.Verify(m => m.Delete(It.IsAny<EmployeeModel>()), Times.Once);
			this.employeesRepository.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public async Task Delete_Employee_Save_Is_False_Throw_Exception_Test()
		{
			try
			{
				this.employeesRepository.Setup(m => m.Save()).Returns(Task.FromResult(false));

				EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

				IActionResult result = await target.Delete(1);

				Assert.Fail("Should be an exception");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Failed to delete employee 1", ex.Message);

				this.employeesRepository.Verify(m => m.Delete(It.IsAny<EmployeeModel>()), Times.Once);
				this.employeesRepository.Verify(m => m.Save(), Times.Once);
			}
		}

		[TestMethod]
		public async Task Add_Employee_Success_Return_CreatedAtAction_Test()
		{
			EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

			IActionResult result = await target.Add(new EmployeeModel());

			var createdAtActionObject = result as CreatedAtActionResult;

			Assert.IsNotNull(createdAtActionObject);

			this.employeesRepository.Verify(m => m.Add(It.IsAny<EmployeeModel>()), Times.Once);
			this.employeesRepository.Verify(m => m.Save(), Times.Once);
		}

		[TestMethod]
		public async Task Add_Employee_Save_Is_False_Throw_Exception_Test()
		{
			try
			{
				this.employeesRepository.Setup(m => m.Save()).Returns(Task.FromResult(false));

				EmployeesController target = new EmployeesController(this.employeesRepository.Object, this.mapper.Object);

				IActionResult result = await target.Add(new EmployeeModel());

				Assert.Fail("Should be an exception");
			}
			catch (Exception ex)
			{
				Assert.AreEqual("Failed to add new employee", ex.Message);

				this.employeesRepository.Verify(m => m.Add(It.IsAny<EmployeeModel>()), Times.Once);
				this.employeesRepository.Verify(m => m.Save(), Times.Once);
			}
		}

		[TestInitialize]
		public void Init()
		{
			var employees = new List<EmployeeModel>()
			{
				new EmployeeModel
				{
					Id = 1,
					Birth = DateTime.Now.AddYears(-10),
					FirstName = "John",
					LastName = "Doe",
					Salary = 1000,
					TaxNumber = 1234,
					WorkingPosition = "Developer"
				},
				new EmployeeModel
				{
					Id = 2,
					Birth = DateTime.Now.AddYears(-20),
					FirstName = "Edward",
					LastName = "Kovalsky",
					Salary = 2000,
					TaxNumber = 0987,
					WorkingPosition = "CEO"
				}
			};

			var employeesForList = new List<EmployeeForListDto>()
			{
				new EmployeeForListDto
				{
					Id = 1,
					Age = 10,
					FirstName = "John",
					LastName = "Doe",
					Salary = 1000,
					TaxNumber = 1234,
					WorkingPosition = "Developer"
				},
				new EmployeeForListDto
				{
					Id = 2,
					Age = 20,
					FirstName = "Edward",
					LastName = "Kovalsky",
					Salary = 2000,
					TaxNumber = 0987,
					WorkingPosition = "CEO"
				}
			};

			var employee = new EmployeeModel
			{
				Id = 1,
				Birth = DateTime.Now.AddYears(-10),
				FirstName = "John",
				LastName = "Doe",
				Salary = 1000,
				TaxNumber = 1234,
				WorkingPosition = "Developer"
			};

			var employeeForList = new EmployeeForListDto
			{
				Id = 1,
				Age = 10,
				FirstName = "John",
				LastName = "Doe",
				Salary = 1000,
				TaxNumber = 1234,
				WorkingPosition = "Developer"
			};

			var eRMock = new Mock<IEmployeesRepository>(MockBehavior.Strict);

			eRMock.Setup(m => m.Get()).Returns(Task.FromResult(employees));

			eRMock.Setup(m => m.Get(1)).Returns(Task.FromResult(employee));

			eRMock.Setup(m => m.Save()).Returns(Task.FromResult(true));

			eRMock.Setup(m => m.Update(It.IsAny<EmployeeModel>(), It.IsAny<EmployeeModel>()));

			eRMock.Setup(m => m.Add(It.IsAny<EmployeeModel>()));

			eRMock.Setup(m => m.Delete(It.IsAny<EmployeeModel>()));

			var mapMock = new Mock<IMapper>(MockBehavior.Strict);

			mapMock.Setup(m => m.Map<IEnumerable<EmployeeForListDto>>(employees)).Returns(employeesForList);
			mapMock.Setup(m => m.Map<EmployeeForListDto>(employee)).Returns(employeeForList);

			this.employeesRepository = eRMock;
			this.mapper = mapMock;
		}
	}
}
