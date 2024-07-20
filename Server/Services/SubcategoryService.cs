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

        public async Task<int> CountPromotions()
        {
            return await _dbContext.Subcategories.Where(s => s.ActivePromotion == true).CountAsync();
        }
        public async Task<SubcategoryDTO> Create(SubcategoryDTO subcategoryDTO)
		{
			var subCategoryInDb = await _dbContext.Subcategories.FirstOrDefaultAsync(c => c.Id == subcategoryDTO.Id);
			if (subCategoryInDb != null)
			{
				throw new AppException("This subcategory is already exist");
			}

			var subcategory = _mapper.Map<Subcategory>(subcategoryDTO);

			await _dbContext.Subcategories.AddAsync(subcategory);
			await _dbContext.SaveChangesAsync();
			return _mapper.Map<SubcategoryDTO>(subcategory);
		}

		public async Task<bool> Delete(Guid subcategoryId)
		{
			var subcategory = await _dbContext.Subcategories.FindAsync(subcategoryId);
			if (subcategory == null)
			{
				throw new AppException("Subcategory not found");
			}

			_dbContext.Subcategories.Remove(subcategory);
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
            return await _dbContext.Subcategories.Where(c => c.ActivePromotion == true).OrderByDescending(c => c.UpdatedAt).ProjectTo<SubcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<SubcategoryDTO>> Search(string input)
		{
			return await _dbContext.Subcategories.Include(c => c.Category).Where(c => c.Name.ToLower().Contains(input) || c.Category.Name.ToLower().Contains(input)).OrderByDescending(c => c.Name).ProjectTo<SubcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<SubcategoryDTO> Update(SubcategoryDTO subcategoryDTO)
		{
			var subcategory = await _dbContext.Subcategories.FindAsync(subcategoryDTO.Id);
			if (subcategory == null)
			{
				throw new AppException("Subcategory not found");
			}

			subcategory.Name = subcategoryDTO.Name;
			subcategory.UpdatedAt = DateTime.Now;
			subcategory.ActivePromotion = subcategoryDTO.ActivePromotion;
			subcategory.PromotionName = subcategoryDTO.PromotionName;
			subcategory.PromotionDiscount = subcategoryDTO.PromotionDiscount;
			subcategory.PromotionDurationHour = subcategoryDTO.PromotionDurationHour;
			subcategory.PromotionStartedAt = subcategoryDTO.PromotionStartedAt;

			await _dbContext.SaveChangesAsync();

			return _mapper.Map<SubcategoryDTO>(subcategory);
		}
		public async Task<bool> CheckPromotion()
		{
            var activePromotions = await _dbContext.Subcategories.Where(s => s.ActivePromotion == true).ToListAsync();
            foreach (var activePromotion in activePromotions)
            {
                DateTime startTime = (DateTime)activePromotion.PromotionStartedAt;
                DateTime endTime = startTime.AddHours((double)activePromotion.PromotionDurationHour);
                if (DateTime.Now > endTime)
                {
                    activePromotion.ActivePromotion = false;
                    activePromotion.PromotionName = null;
                    activePromotion.PromotionDiscount = null;
                    activePromotion.PromotionDurationHour = null;
                    activePromotion.PromotionStartedAt = null;
                }
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> StartPromotion(SaleDTO saleDTO)
        {
            var subcategory = await _dbContext.Subcategories.FirstOrDefaultAsync(s => s.Id == saleDTO.CategoryId);
            if (subcategory.ActivePromotion)
            {
                return false;
            }

            subcategory.ActivePromotion = true;
            subcategory.PromotionName = saleDTO.PromotionName;
            subcategory.PromotionDiscount = saleDTO.Discount;
            subcategory.PromotionDurationHour = saleDTO.Duration;
            subcategory.PromotionStartedAt = DateTime.Now;
            subcategory.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EndPromotion(Guid subcategoryId)
        {
            var subcategory = await _dbContext.Subcategories.FirstOrDefaultAsync(s => s.Id == subcategoryId);
            if (subcategory is null)
            {
                return false;
            }

            subcategory.ActivePromotion = false;
            subcategory.PromotionName = null;
            subcategory.PromotionDiscount = null;
            subcategory.PromotionDurationHour = null;
            subcategory.PromotionStartedAt = null;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<SubcategoryDTO>> GetPromotions(int page)
        {
            int skip = (page - 1) * _size;
            return await _dbContext.Subcategories.Where(s => s.ActivePromotion == true).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<SubcategoryDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
