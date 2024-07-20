using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface ISub_subcategoryService
	{
		public Task<int> Count();
		public Task<IEnumerable<Sub_subcategoryDTO>> Get();
		public Task<IEnumerable<Sub_subcategoryDTO>> GetByPage(int page);
		public Task<IEnumerable<Sub_subcategoryDTO>> Search(string input);
		public Task<IEnumerable<Sub_subcategoryDTO>> SortBy(int page, string option, string sortingOrder);
		public Task<Sub_subcategoryDTO> GetByID(Guid subSubCategoryId);
		public Task<Sub_subcategoryDTO> Create(Sub_subcategoryDTO subSubcategoryDTO);
		public Task<Sub_subcategoryDTO> Update(Sub_subcategoryDTO subSubcategoryDTO);
		public Task<bool> Delete(Guid subcategoryId);
	}
}
