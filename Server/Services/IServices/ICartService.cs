using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface ICartService
	{
        public Task<CartDTO> Get(Guid userId);
		public Task<CartDTO> Add(Guid userId, ProductInCartDTO productDTO, int quantity = 1);
		public Task<CartDTO> Remove(Guid userId, ProductInCartDTO productDTO, int quantity = 1);
		public Task<CartDTO> Clear(Guid userId);
	}
}
