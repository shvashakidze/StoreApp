using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreApiProject.Model;
using StoreApiProject.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreApiProject.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private IUserRepository _userRepository;
		public IConfiguration _configuration { get; set; }
		public string connectionSt;

		public EmployeeController(IUserRepository userRepository, IConfiguration configuration)
		{
			this._userRepository = userRepository;
			_configuration = configuration;
			connectionSt = _configuration.GetConnectionString("DefaultConnection");

		}

		[HttpPost]
		[Authorize]
		public IActionResult SaveEmployee(Employee employee)

		{
			if (employee == null ||
	           
	           string.IsNullOrWhiteSpace(employee.username) ||
	           string.IsNullOrWhiteSpace(employee.password) ||
	           string.IsNullOrWhiteSpace(employee.firstname) ||
	           string.IsNullOrWhiteSpace(employee.lastname) 
	            )
			{
				return BadRequest("Employee data is empty or incomplete");
			}

			var adminId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);


			try { 
				var employeeObj = new Employee
				{
				    

		           adminID = adminId,
                   terminalNumber = employee.terminalNumber,
                   username = employee.username,
                   password = employee.password,
                   firstname = employee.firstname,
                   lastname = employee.lastname,
                   role = employee.role

	            };

				_userRepository.CreateEmployee(employeeObj);

				return Ok(employeeObj);
			}
			catch
			{
				return StatusCode(500, "system error");
			}



		}

		[HttpGet]
		[Authorize]
		public IActionResult GetEmployees()
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

			try { 
			List<Employee> employees = _userRepository.GetEmployees(userId);

			return Ok(employees);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetUsers()
		{
			var adminId = User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value;

			try { 
			List<Employee> employees = _userRepository.GetUsers(adminId);

			return Ok(employees);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpDelete]
		[Authorize]
		public IActionResult DeleteEmployee(int id)
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

			_userRepository.DeleteEmployee(id, userId);

			return Ok();
		}
		[HttpPut]
		[Authorize]
		public IActionResult UpdateEmployee(Employee employee)
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
			try { 
			_userRepository.UpdateEmployee(employee, userId);

			return Ok();
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetUserById(int id)
		{
			
			var userId = User.Claims.First(x => x.Type == ClaimTypes.HomePhone).Value;

			try
			{
				List<Employee> UserById = _userRepository.GetUserById(userId, id);

				return Ok(UserById);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

	}
}
