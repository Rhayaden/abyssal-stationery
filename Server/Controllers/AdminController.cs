using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService _adminService;

		public AdminController(IAdminService adminService)
        {
			_adminService = adminService;
		}

		[HttpGet("Check/{emailAsClaim}")]
		public async Task<ServiceResponse<bool>> Check(string emailAsClaim)
		{
			return new ServiceResponse<bool>()
			{
				Data = await _adminService.IsAdmin(emailAsClaim),
			};
		}

		[HttpPost("Login")]
		public async Task<ServiceResponse<AdminLoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
		{
			return new ServiceResponse<AdminLoginResponseDTO>()
			{
				Data = await _adminService.Login(loginRequestDTO),
			};
		}
	}
}
