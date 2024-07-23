using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleSmsSenderApp.Models;
using SimpleSmsSenderApp.Services;

namespace SimpleSmsSenderApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SmsController : ControllerBase
	{
		private readonly ISmsService _smsService;

		public SmsController(ISmsService smsService)
		{
			_smsService = smsService;
		}


		[HttpPost("SendSms")]
		public async Task<IActionResult> SendSms([FromQuery] string phoneNumber, string message)
		{
			await _smsService.SendSms(phoneNumber,message);
			return Ok();
		}
	}
}
