using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface IProductService
	{
        public Task<int> Count();
        public Task<IEnumerable<ProductDTO>> Get();
        public Task<IEnumerable<ProductDTO>> GetByPage(int page);
        public Task<IEnumerable<ProductDTO>> Search(string input);
		public Task<ProductDTO> GetByID(Guid productId);
        public Task<IEnumerable<ProductDTO>> GetByCategory(Guid categoryId);
		public Task<ProductDTO> Create(ProductDTO productDTO);
		public Task<ProductDTO> Update(ProductDTO productDTO);
		public Task<bool> Delete(Guid productId);
	}
}
