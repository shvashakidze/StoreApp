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
	public class UsersController : ControllerBase
	{
		private IUserRepository _userRepository;
		public IConfiguration _configuration { get; set; }
		public string connectionSt;

		public UsersController(IUserRepository userRepository, IConfiguration configuration)
		{
			this._userRepository = userRepository;
			_configuration = configuration;
			connectionSt = _configuration.GetConnectionString("DefaultConnection");

		}

		[HttpPost]
		public IActionResult Login(LoginAdmin user)
		{
			if (string.IsNullOrWhiteSpace(user.password) || string.IsNullOrWhiteSpace(user.username))
			{
				return BadRequest("data is empty");
			}
			try { 

			var userData = _userRepository.Login(user); 

			if (userData == null)
			{
				return Unauthorized(); 
			}

			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, userData.id.ToString()),
		new Claim(ClaimTypes.HomePhone, userData.adminID.ToString()),
		new Claim(ClaimTypes.SerialNumber, userData.terminalNumber.ToString()),
		new Claim(ClaimTypes.Role, userData.role.ToString())
	};

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("thissssssssssssssssssssssiismykey");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return Ok(new { token = tokenString });
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

	

		

		[HttpPost]
		public IActionResult CreateUser(User user)
		{

			if (user == null )
			{
				return BadRequest("user data is empty");
			}

			try { 
			_userRepository.CreateOrganization(user);

				return Ok();
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		
	}
}
