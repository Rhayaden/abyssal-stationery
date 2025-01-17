﻿using Blazor.Shared.DTOs;

namespace Blazor.Server.Services.IServices
{
	public interface ICategoryService
	{
        public Task<int> Count();
        public Task<IEnumerable<CategoryDTO>> Get();
        public Task<IEnumerable<CategoryDTO>> GetByPage(int page);
        public Task<IEnumerable<CategoryDTO>> Search(string input);
        public Task<CategoryDTO> GetByID(Guid categoryId);
		public Task<IEnumerable<CategoryDTO>> SortBy(int page, string option, string sortingOrder);
		public Task<CategoryDTO> Create(CategoryDTO categoryDTO);
		public Task<CategoryDTO> Update(CategoryDTO categoryDTO);
		public Task<bool> Delete(Guid categoryId);
	}
}
