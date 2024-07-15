using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
        {
			_authService = authService;
		}
		[HttpPost("Login")]
		public async Task<ServiceResponse<LoginResponseDTO>> Login(LoginRequestDTO request)
		{
			return new ServiceResponse<LoginResponseDTO>()
			{
				Data = await _authService.Login(request),
			};
		}
	}
}
