using Blazor.Server.Services;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
        {
			_userService = userService;
		}
		[HttpGet("Count")]
		public async Task<ServiceResponse<int>> Count()
		{
            return new ServiceResponse<int>()
            {
                Data = await _userService.Count(),
            };
        }
		[HttpGet("GetAll")]
		public async Task<ServiceResponse<IEnumerable<UserDTO>>> GetAll()
		{
            return new ServiceResponse<IEnumerable<UserDTO>>()
            {
                Data = await _userService.GetAll(),
            };
        }
        [HttpGet("GetUserPage/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<UserDTO>>> GetByPage(int pageNumber)
        {
            return new ServiceResponse<IEnumerable<UserDTO>>()
            {
                Data = await _userService.GetByPage(pageNumber),
            };
        }
        [HttpGet("Get/{userId}")]
		public async Task<ServiceResponse<UserDTO>> GetById(Guid userId)
		{
			return new ServiceResponse<UserDTO>()
			{
				Data = await _userService.GetByID(userId),
			};
		}
        [HttpGet("SortBy/{option}/{sortingOrder}/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<UserDTO>>> SortBy(int pageNumber, string option, string sortingOrder)
        {
            return new ServiceResponse<IEnumerable<UserDTO>>()
            {
                Data = await _userService.SortBy(pageNumber, option, sortingOrder),
            };
        }
        [AllowAnonymous]
		[HttpPost("Create")]
		public async Task<ServiceResponse<UserDTO>> Create([FromBody] UserDTO userDTO)
		{
			return new ServiceResponse<UserDTO>()
			{
				Data = await _userService.Create(userDTO)
			};
		}

		[HttpPut("Update")]
		public async Task<ServiceResponse<UserDTO>> Update([FromBody] UserDTO userDTO)
		{
			return new ServiceResponse<UserDTO>()
			{
				Data = await _userService.Update(userDTO)
			};
		}

		[HttpDelete("Delete/{userId}")]
		public async Task<ServiceResponse<UserDTO>> Delete(Guid userId)
		{
			var deletedUser = await _userService.GetByID(userId);
			if (deletedUser != null)
			{
				await _userService.Delete(userId);
			}
			return new ServiceResponse<UserDTO>()
			{
				Data = deletedUser
			};
		}
	}
}
