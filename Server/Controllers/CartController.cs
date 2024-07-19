using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
        {
			_cartService = cartService;
		}
		[HttpGet("Get/{userId}")]
		public async Task<ServiceResponse<CartDTO>> Get(Guid userId)
		{
			return new ServiceResponse<CartDTO>()
			{
				Data = await _cartService.Get(userId)
			};
		}

		[HttpPost("Add/{userId}/{quantity}")]
		public async Task<ServiceResponse<CartDTO>> Add(Guid userId, [FromBody] ProductInCartDTO productDTO, int quantity = 1)
		{
			return new ServiceResponse<CartDTO>()
			{
				Data = await _cartService.Add(userId, productDTO, quantity)
			};
		}
		[HttpPost("Remove/{userId}/{quantity}")]
		public async Task<ServiceResponse<CartDTO>> Remove(Guid userId, [FromBody] ProductInCartDTO productDTO, int quantity = 1)
		{
			return new ServiceResponse<CartDTO>()
			{
				Data = await _cartService.Remove(userId, productDTO, quantity)
			};
		}
        [HttpPost("Update/{userId}")]
        public async Task<ServiceResponse<CartDTO>> Update(Guid userId, [FromBody] ProductInCartDTO productDTO)
        {
            return new ServiceResponse<CartDTO>()
            {
                Data = await _cartService.Update(userId, productDTO)
            };
        }
        [HttpPost("Clear/{userId}")]
		public async Task<ServiceResponse<CartDTO>> Clear(Guid userId)
		{
			return new ServiceResponse<CartDTO>()
			{
				Data = await _cartService.Clear(userId)
			};
		}
	}
}
