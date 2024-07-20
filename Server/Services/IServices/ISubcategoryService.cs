using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface ISubcategoryService
	{
		public Task<int> Count();
        public Task<int> CountPromotions();
        public Task<bool> CheckPromotion();
        public Task<IEnumerable<SubcategoryDTO>> GetPromotions();
        public Task<IEnumerable<SubcategoryDTO>> GetPromotions(int page);
        public Task<bool> StartPromotion(SaleDTO saleDTO);
        public Task<bool> EndPromotion(Guid subcategoryId);
        public Task<IEnumerable<SubcategoryDTO>> Get();
		public Task<IEnumerable<SubcategoryDTO>> GetByPage(int page);
		public Task<IEnumerable<SubcategoryDTO>> Search(string input);
		public Task<SubcategoryDTO> GetByID(Guid subCategoryId);
		public Task<IEnumerable<SubcategoryDTO>> SortBy(int page, string option, string sortingOrder, string selection);
		public Task<SubcategoryDTO> Create(SubcategoryDTO subcategoryDTO);
		public Task<SubcategoryDTO> Update(SubcategoryDTO subcategoryDTO);
		public Task<bool> Delete(Guid subcategoryId);
	}
}
