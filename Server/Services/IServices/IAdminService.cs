using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface IAdminService
	{
		public Task<bool> IsAdmin(string emailAsClaim);
		public Task<AdminLoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
	}
}
