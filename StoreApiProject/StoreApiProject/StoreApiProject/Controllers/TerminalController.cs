using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreApiProject.Model;
using StoreApiProject.Repositories;
using System.Security.Claims;

namespace StoreApiProject.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TerminalController : ControllerBase
	{
		private ITerminalRepository _terminalRepository;

		public TerminalController(ITerminalRepository terminalRepository)
		{
			this._terminalRepository = terminalRepository;
		}
		[HttpPost]
		[Authorize]
		public IActionResult SaveTerminal(Terminal terminal)

		{

			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

			if (terminal == null)
			{
				return BadRequest("terminal data is empty");
			}

			try { 
				var terminalObj = new Terminal
				{
					terminalNumber = terminal.terminalNumber,
					adminID = userId,
				};

				_terminalRepository.CreateTerminal(terminalObj);

				return Ok(terminalObj);
		}
		catch
			{
				return StatusCode(500, "system error");
	        }
}

		[HttpDelete]
		[Authorize]
		public IActionResult DeleteTerminal(int id)
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

			try
			{
				_terminalRepository.DeleteTerminal(id, userId);

				return Ok();
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}

		[HttpGet]
		[Authorize]
		public IActionResult GetTerminals()
		{
			var userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

			try { 
			List<Terminal> terminalList = _terminalRepository.GetTerminallist(userId);

			return Ok(terminalList);
			}
			catch
			{
				return StatusCode(500, "system error");
			}
		}
	}
}
