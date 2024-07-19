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
	public class SubcategoryService : ISubcategoryService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
		private readonly int _size;

		public SubcategoryService(IMapper mapper, AppDbContext dbContext)
		{
			_mapper = mapper;
			_dbContext = dbContext;
			_size = ItemsPerPage.SubCategory;
		}

		public async Task<int> Count()
		{
			return await _dbContext.Subcategories.CountAsync();
		}
		public async Task<SubcategoryDTO> Create(SubcategoryDTO subcategoryDTO)
		{
			var subCategoryInDb = await _dbContext.Subcategories.FirstOrDefaultAsync(c => c.Id == subcategoryDTO.Id);
			if (subCategoryInDb != null)
			{
				throw new AppException("This sub-category is already exist");
			}

			var subcategory = _mapper.Map<Subcategory>(subcategoryDTO);

			await _dbContext.Subcategories.AddAsync(subcategory);
			await _dbContext.SaveChangesAsync();
			return _mapper.Map<SubcategoryDTO>(subcategory);
		}

		public async Task<bool> Delete(Guid subCategoryId)
		{
			var subCategory = await _dbContext.Subcategories.FindAsync(subCategoryId);
			if (subCategory == null)
			{
				throw new AppException("Sub-category not found");
			}

			_dbContext.Subcategories.Remove(subCategory);
			var result = await _dbContext.SaveChangesAsync();
			return result > 0;
		}

		public async Task<IEnumerable<SubcategoryDTO>> Get()
		{
			return await _dbContext.Subcategories.Include(c => c.Subsubcategories).OrderBy(c => c.Name).ProjectTo<SubcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<SubcategoryDTO> GetByID(Guid subCategoryId)
		{
			var subCategory = await _dbContext.Subcategories.FindAsync(subCategoryId);

			return _mapper.Map<SubcategoryDTO>(subCategory);
		}

		public async Task<IEnumerable<SubcategoryDTO>> GetByPage(int page)
		{
			int skip = (page - 1) * _size;
			return await _dbContext.Subcategories.Include(c => c.Category).OrderBy(c => c.Name).Skip(skip).Take(_size).ProjectTo<SubcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

        public async Task<IEnumerable<SubcategoryDTO>> GetPromotions()
        {
            return await _dbContext.Subcategories.Where(c => c.ActivePromotion == true).OrderBy(c => c.UpdatedAt).ProjectTo<SubcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<SubcategoryDTO>> Search(string input)
		{
			return await _dbContext.Subcategories.Include(c => c.Category).Where(c => c.Name.ToLower().Contains(input) || c.Category.Name.ToLower().Contains(input)).OrderByDescending(c => c.Name).ProjectTo<SubcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<SubcategoryDTO> Update(SubcategoryDTO subCategoryDTO)
		{
			var subCategory = await _dbContext.Subcategories.FindAsync(subCategoryDTO.Id);
			if (subCategory == null)
			{
				throw new AppException("Sub-category not found");
			}

			subCategory.Name = subCategoryDTO.Name;
			subCategory.UpdatedAt = DateTime.Now;
			subCategory.ActivePromotion = subCategoryDTO.ActivePromotion;
			subCategory.PromotionName = subCategoryDTO.PromotionName;

			await _dbContext.SaveChangesAsync();

			return _mapper.Map<SubcategoryDTO>(subCategory);
		}
	}
}
