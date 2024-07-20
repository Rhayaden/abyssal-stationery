using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface IProductService
	{
        public Task<int> Count();
        public Task<int> CountByMainCategory(Guid categoryId);
        public Task<int> CountBySubcategory(Guid subcategoryId);
        public Task<int> CountBySubsubcategory(Guid subSubcategoryId);
        public Task<int> CountOnSales();
        public Task<bool> CheckSale();
        public Task<IEnumerable<ProductDTO>> Get();
        public Task<IEnumerable<ProductDTO>> GetAllSales();
        public Task<IEnumerable<ProductDTO>> GetAllPromotions();
        public Task<IEnumerable<ProductDTO>> GetOnSales(int page);
        public Task<IEnumerable<ProductDTO>> GetPromotions(int page);
        public Task<bool> StartPromotion(SaleDTO saleDTO);
        public Task<bool> EndPromotion(Guid categoryId);
        public Task<IEnumerable<ProductDTO>> GetByPage(int page);
        public Task<IEnumerable<ProductDTO>> Search(string input);
        public Task<IEnumerable<ProductDTO>> SearchByCategory(string input);
		public Task<ProductDTO> GetByID(Guid productId);
        public Task<IEnumerable<ProductDTO>> GetByMainCategory(Guid categoryId, int page);
        public Task<IEnumerable<ProductDTO>> GetBySubcategory(Guid subcategoryId, int page);
        public Task<IEnumerable<ProductDTO>> GetBySubsubcategory(Guid subSubcategoryId, int page);
        public Task<IEnumerable<ProductDTO>> GetBySubcategoryByPage(Guid subcategoryId, int page);
        public Task<IEnumerable<ProductDTO>> SortBy(int page, string option, string sortingOrder, string selection);
		public Task<ProductDTO> Create(ProductDTO productDTO);
		public Task<ProductDTO> Update(ProductDTO productDTO);
		public Task<bool> Delete(Guid productId);
	}
}
