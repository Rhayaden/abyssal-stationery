using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface IOrderService
	{
        public Task<int> Count();
        public Task<int> CountByUser(Guid userId);
        public Task<IEnumerable<OrderDTO>> GetAll(Guid userId);
        public Task<IEnumerable<OrderDTO>> GetAllByPage(Guid userId, int page);
		public Task<OrderDTO> GetByID(Guid orderId);
		public Task<IEnumerable<OrderDTO>> SortBy(int page, string option, string sortingOrder);
		public Task<OrderDTO> Order(CartDTO cartDTO);
	}
}
