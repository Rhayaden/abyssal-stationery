using Blazor.Server.Services;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
        {
			_orderService = orderService;
		}
		[HttpGet("Count")]
		public async Task<ServiceResponse<int>> Count()
        {
            return new ServiceResponse<int>()
            {
                Data = await _orderService.Count(),
            };
        }
        [HttpGet("Count/{userId}")]
        public async Task<ServiceResponse<int>> CountByUser(Guid userId)
        {
            return new ServiceResponse<int>()
            {
                Data = await _orderService.CountByUser(userId),
            };
        }
        [HttpGet("GetAll/{userId}")]
		public async Task<ServiceResponse<IEnumerable<OrderDTO>>> Get(Guid userId)
		{
			return new ServiceResponse<IEnumerable<OrderDTO>>()
			{
				Data = await _orderService.GetAll(userId),
			};
		}
        [HttpGet("GetAll/{userId}/Page/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetByPage(Guid userId, int pageNumber)
        {
            return new ServiceResponse<IEnumerable<OrderDTO>>()
            {
                Data = await _orderService.GetAllByPage(userId, pageNumber),
            };
        }

        [HttpGet("Get/{orderId}")]
		public async Task<ServiceResponse<OrderDTO>> GetById(Guid orderId)
		{
			return new ServiceResponse<OrderDTO>()
			{
				Data = await _orderService.GetByID(orderId),
			};
		}
		[HttpGet("SortBy/{option}/{sortingOrder}/{pageNumber}")]
		public async Task<ServiceResponse<IEnumerable<OrderDTO>>> SortBy(int pageNumber, string option, string sortingOrder)
		{
			return new ServiceResponse<IEnumerable<OrderDTO>>()
			{
				Data = await _orderService.SortBy(pageNumber, option, sortingOrder),
			};
		}
		[HttpPost("Checkout")]
		public async Task<ServiceResponse<OrderDTO>> Order(CartDTO cartDTO)
		{
			return new ServiceResponse<OrderDTO>()
			{
				Data = await _orderService.Order(cartDTO),
			};
		}
	}
}
