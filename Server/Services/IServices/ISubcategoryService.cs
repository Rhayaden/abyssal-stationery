using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface ISubcategoryService
	{
		public Task<int> Count();
		public Task<IEnumerable<SubcategoryDTO>> GetPromotions();
		public Task<IEnumerable<SubcategoryDTO>> Get();
		public Task<IEnumerable<SubcategoryDTO>> GetByPage(int page);
		public Task<IEnumerable<SubcategoryDTO>> Search(string input);
		public Task<SubcategoryDTO> GetByID(Guid subCategoryId);
		public Task<SubcategoryDTO> Create(SubcategoryDTO subcategoryDTO);
		public Task<SubcategoryDTO> Update(SubcategoryDTO subcategoryDTO);
		public Task<bool> Delete(Guid subcategoryId);
	}
}
