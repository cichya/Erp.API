using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Erp.API.DTOs;
using Erp.API.Models;
using Erp.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Erp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
		private readonly IEmployeesRepository employeesRepository;
		private readonly IMapper mapper;

		public EmployeesController(IEmployeesRepository employeesRepository, IMapper mapper)
		{
			this.employeesRepository = employeesRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetEmployees()
		{
			var employees = await this.employeesRepository.Get();

			var employeesToReturn = this.mapper.Map<IEnumerable<EmployeeForListDto>>(employees);

			return this.Ok(employeesToReturn);
		}

		[HttpGet("{id}", Name = "GetEmployee")]
		public async Task<IActionResult> GetEmployee(int id)
		{
			var employee = await this.employeesRepository.Get(id);

			if (employee == null)
			{
				return NotFound();
			}

			var employeeForReturn = this.mapper.Map<EmployeeForListDto>(employee);

			return this.Ok(employeeForReturn);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateEmployee(int id, EmployeeModel employee)
		{
			if (id != employee.Id)
			{
				return this.BadRequest();
			}

			var emp = await this.employeesRepository.Get(id);

			if (emp == null)
			{
				return this.NotFound();
			}

			this.employeesRepository.Update(emp, employee);

			if (await this.employeesRepository.Save())
			{
				return this.NoContent();
			}

			throw new Exception($"Failed to update user {id}");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var employee = await this.employeesRepository.Get(id);

			if (employee == null)
			{
				return this.NotFound();
			}

			this.employeesRepository.Delete(employee);

			if (await this.employeesRepository.Save())
			{
				return this.NoContent();
			}

			throw new Exception($"Failed to delete user {id}");
		}

		[HttpPost]
		public async Task<IActionResult> Add(EmployeeModel employee)
		{
			this.employeesRepository.Add(employee);

			if (await this.employeesRepository.Save())
			{
				return this.NoContent();
			}

			return this.CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
		}
    }
}