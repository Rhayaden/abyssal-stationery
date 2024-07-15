using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface IAuthService
	{
		public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
	}
}
