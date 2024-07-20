using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blazor.Server.Data;
using Blazor.Server.Models;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
{
    public class CategoryService : ICategoryService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
		private readonly int _size;
		public CategoryService(IMapper mapper, AppDbContext dbContext)
        {
			_mapper = mapper;
			_dbContext = dbContext;
			_size = ItemsPerPage.Category;
		}
        public async Task<int> Count()
        {
            return await _dbContext.Categories.CountAsync();
        }
        public async Task<CategoryDTO> Create(CategoryDTO categoryDTO)
		{
			var categoryInDb = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryDTO.Id);
			if (categoryInDb != null)
			{
				throw new AppException("This category is already exist");
			}

			var category = _mapper.Map<Category>(categoryDTO);

			await _dbContext.Categories.AddAsync(category);
			await _dbContext.SaveChangesAsync();
			return _mapper.Map<CategoryDTO>(category);
		}

		public async Task<bool> Delete(Guid categoryId)
		{
			var category = await _dbContext.Categories.FindAsync(categoryId);
			if (category == null)
			{
				throw new AppException("Category not found");
			}

			_dbContext.Categories.Remove(category);
			var result = await _dbContext.SaveChangesAsync();
			return result > 0;
		}

		public async Task<IEnumerable<CategoryDTO>> Get()
		{
			return await _dbContext.Categories.Include(c => c.Subcategories).OrderBy(c => c.Name).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<CategoryDTO> GetByID(Guid categoryId)
		{
			var category = await _dbContext.Categories.FindAsync(categoryId);

			return _mapper.Map<CategoryDTO>(category);
		}

		public async Task<IEnumerable<CategoryDTO>> GetByPage(int page)
		{
			int skip = (page - 1) * _size;
			return await _dbContext.Categories.Include(c => c.Subcategories).OrderBy(c => c.Name).Skip(skip).Take(_size).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

        public async Task<IEnumerable<CategoryDTO>> Search(string input)
        {
            return await _dbContext.Categories.Include(c => c.Subcategories).Where(c => c.Name.ToLower().Contains(input)).OrderByDescending(c => c.Name).ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

		public async Task<IEnumerable<CategoryDTO>> SortBy(int page, string option, string sortingOrder)
		{
			int skip = (page - 1) * _size;
			var categories = await _dbContext.Categories.ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
			IEnumerable<CategoryDTO> sortedList = new List<CategoryDTO>();
			switch (option.ToLower())
			{
				case "name":
					if (sortingOrder == "desc")
					{
						sortedList = categories.OrderByDescending(c => c.Name).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = categories.OrderBy(c => c.Name).Skip(skip).Take(_size);
					}
					break;
				case "date":
					if (sortingOrder == "desc")
					{
						sortedList = categories.OrderByDescending(c => c.UpdatedAt).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = categories.OrderBy(c => c.UpdatedAt).Skip(skip).Take(_size);
					}
					break;
			}

			return sortedList;
		}

		public async Task<CategoryDTO> Update(CategoryDTO categoryDTO)
		{
			var category = await _dbContext.Categories.FindAsync(categoryDTO.Id);
			if (category == null)
			{
				throw new AppException("Category not found");
			}

			category.Name = categoryDTO.Name;
			category.UpdatedAt = DateTime.Now;

			await _dbContext.SaveChangesAsync();

			return _mapper.Map<CategoryDTO>(category);
		}
	}
}
