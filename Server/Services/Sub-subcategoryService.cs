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
	public class Sub_subcategoryService : ISub_subcategoryService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
		private readonly int _size;

		public Sub_subcategoryService(IMapper mapper, AppDbContext dbContext)
        {
			_mapper = mapper;
			_dbContext = dbContext;
			_size = ItemsPerPage.SubCategory;
		}
		public async Task<int> Count()
		{
			return await _dbContext.Subsubcategories.CountAsync();
		}
		public async Task<Sub_subcategoryDTO> Create(Sub_subcategoryDTO subSubcategoryDTO)
		{
			var subSubCategoryInDb = await _dbContext.Subsubcategories.FirstOrDefaultAsync(c => c.Id == subSubcategoryDTO.Id);
			if (subSubCategoryInDb != null)
			{
				throw new AppException("This sub-category is already exist");
			}

			var subSubcategory = _mapper.Map<Sub_subcategory>(subSubcategoryDTO);

			await _dbContext.Subsubcategories.AddAsync(subSubcategory);
			await _dbContext.SaveChangesAsync();
			return _mapper.Map<Sub_subcategoryDTO>(subSubcategory);
		}

		public async Task<bool> Delete(Guid subSubCategoryId)
		{
			var subbSubCategory = await _dbContext.Subsubcategories.FindAsync(subSubCategoryId);
			if (subbSubCategory == null)
			{
				throw new AppException("Sub-category not found");
			}

			_dbContext.Subsubcategories.Remove(subbSubCategory);
			var result = await _dbContext.SaveChangesAsync();
			return result > 0;
		}

		public async Task<IEnumerable<Sub_subcategoryDTO>> Get()
		{
			return await _dbContext.Subsubcategories.Include(c => c.Subcategory).OrderBy(c => c.Name).ProjectTo<Sub_subcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<Sub_subcategoryDTO> GetByID(Guid subSubcategoryId)
		{
			var subSubCategory = await _dbContext.Subsubcategories.FindAsync(subSubcategoryId);

			return _mapper.Map<Sub_subcategoryDTO>(subSubCategory);
		}

		public async Task<IEnumerable<Sub_subcategoryDTO>> GetByPage(int page)
		{
			int skip = (page - 1) * _size;
			return await _dbContext.Subsubcategories.Include(c => c.Subcategory).OrderBy(c => c.Name).Skip(skip).Take(_size).ProjectTo<Sub_subcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<IEnumerable<Sub_subcategoryDTO>> Search(string input)
		{
			return await _dbContext.Subsubcategories.Include(c => c.Subcategory).Where(c => c.Name.ToLower().Contains(input) || c.Subcategory.Name.ToLower().Contains(input)).OrderByDescending(c => c.Name).ProjectTo<Sub_subcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<Sub_subcategoryDTO> Update(Sub_subcategoryDTO subSubcategoryDTO)
		{
			var subSubCategory = await _dbContext.Subsubcategories.FindAsync(subSubcategoryDTO.Id);
			if (subSubCategory == null)
			{
				throw new AppException("Sub-category not found");
			}

			subSubCategory.Name = subSubcategoryDTO.Name;
			subSubCategory.SubcategoryId = subSubcategoryDTO.SubcategoryId;
			subSubCategory.UpdatedAt = DateTime.Now;

			await _dbContext.SaveChangesAsync();

			return _mapper.Map<Sub_subcategoryDTO>(subSubCategory);
		}
	}
}
