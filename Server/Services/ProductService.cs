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
	public class ProductService : IProductService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
        private readonly int _size;
		public ProductService(IMapper mapper, AppDbContext dbContext)
        {
			_mapper = mapper;
			_dbContext = dbContext;
            _size = ItemsPerPage.Product;
		}
        public async Task<int> Count()
        {
            return await _dbContext.Products.CountAsync();
        }
		public async Task<int> CountOnSales()
		{
			return await _dbContext.Products.Where(p => p.OnSale == true).CountAsync();
		}
        public async Task<int> CountByMainCategory(Guid categoryId)
        {
            return await _dbContext.Products.Where(p => p.Subsubcategory.Subcategory.CategoryId == categoryId).CountAsync();
        }
        public async Task<int> CountBySubcategory(Guid subcategoryId)
        {
            return await _dbContext.Products.Where(p => p.Subsubcategory.Subcategory.Id == subcategoryId).CountAsync();
        }
        public async Task<int> CountBySubsubcategory(Guid subSubcategoryId)
        {
            return await _dbContext.Products.Where(p => p.Subsubcategory.Id == subSubcategoryId).CountAsync();
        }
        public async Task<ProductDTO> Create(ProductDTO productDTO)
		{
			var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productDTO.Id);
			if (product != null)
			{
				throw new AppException("This product is already exist");
			}

			var productModel = _mapper.Map<Product>(productDTO);

			await _dbContext.Products.AddAsync(productModel);
			await _dbContext.SaveChangesAsync();

			return _mapper.Map<ProductDTO>(productModel);
		}

		public async Task<bool> Delete(Guid productId)
		{
			var product = await _dbContext.Products.FindAsync(productId);
			if (product == null)
			{
				throw new AppException("Product not found");
			}

			_dbContext.Products.Remove(product);
			var result = await _dbContext.SaveChangesAsync();

			return result > 0;
		}

		public async Task<IEnumerable<ProductDTO>> Get()
		{
			return await _dbContext.Products.Include(p => p.Subsubcategory).OrderByDescending(p => p.UpdatedAt).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<IEnumerable<ProductDTO>> GetByPage(int page)
		{
			int skip = (page - 1) * _size;
			return await _dbContext.Products.Include(p => p.Subsubcategory).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

        public async Task<IEnumerable<ProductDTO>> GetByMainCategory(Guid categoryId, int page)
        {
            int skip = (page - 1) * _size;
            return await _dbContext.Products.Include(p => p.Subsubcategory).Where(p => p.Subsubcategory.Subcategory.CategoryId == categoryId).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetBySubcategory(Guid subcategoryId, int page)
        {
            int skip = (page - 1) * _size;
            return await _dbContext.Products.Include(p => p.Subsubcategory).Where(p => p.Subsubcategory.SubcategoryId == subcategoryId).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetBySubsubcategory(Guid subSubcategoryId, int page)
        {
            int skip = (page - 1) * _size;
            return await _dbContext.Products.Where(p => p.SubsubcategoryId == subSubcategoryId).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ProductDTO> GetByID(Guid productId)
		{
			var product = await _dbContext.Products.FindAsync(productId);

			return _mapper.Map<ProductDTO>(product);
		}

        public async Task<IEnumerable<ProductDTO>> Search(string input)
        {
            return await _dbContext.Products.Where(p => p.Title.ToLower().Contains(input)).OrderByDescending(p => p.UpdatedAt).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
		{
			var product = await _dbContext.Products.FindAsync(productDTO.Id);
			if (product == null)
			{
				throw new AppException("Product not found");
			}

			product.Title = productDTO.Title;
			product.Description = productDTO.Description;
			product.Image = productDTO.Image;
			product.SubsubcategoryId = productDTO.SubsubcategoryId;
			product.Price = productDTO.Price;
			product.OnSale = productDTO.OnSale;
            if (!product.OnSale)
            {
                product.Discount = null;
                product.SalePrice = null;
                product.DurationHour = null;
                product.SaleStartedAt = null;
            }
			else
			{
                product.Discount = productDTO.Discount;
                product.SalePrice = productDTO.SalePrice;
                product.DurationHour = productDTO.DurationHour;
                product.SaleStartedAt = DateTime.Now;
            }
			if(product.OnSale != productDTO.OnSale || product.Stock == productDTO.Stock)
			{
				product.UpdatedAt = DateTime.Now;
			}
			product.Stock = productDTO.Stock;

			await _dbContext.SaveChangesAsync();

			return _mapper.Map<ProductDTO>(product);
		}

        public async Task<IEnumerable<ProductDTO>> GetOnSales(int page)
        {
            int skip = (page - 1) * _size;
            return await _dbContext.Products.Include(p => p.Subsubcategory).Where(p => p.OnSale == true).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> CheckSale()
        {
            var productsOnSale = await _dbContext.Products.Where(p => p.OnSale == true).OrderByDescending(p => p.UpdatedAt).ToListAsync();
			foreach(var product in productsOnSale)
			{
				DateTime startTime = (DateTime)product.SaleStartedAt;
				DateTime endTime = startTime.AddHours((double)product.DurationHour);
                if (DateTime.Now > endTime)
                {
                    product.OnSale = false;
                    product.Discount = null;
                    product.SalePrice = null;
                    product.DurationHour = null;
                    product.SaleStartedAt = null;
                }
				await _dbContext.SaveChangesAsync();
            }
            return true;
        }

		public async Task<IEnumerable<ProductDTO>> GetAllSales()
		{
			await CheckSale();
			return await _dbContext.Products.Include(p => p.Subsubcategory).Where(p => p.OnSale == true).OrderByDescending(p => p.UpdatedAt).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<IEnumerable<ProductDTO>> GetAllPromotions()
		{
			await CheckSale();
			return await _dbContext.Products.Include(p => p.Subsubcategory.Subcategory).Where(p => p.Subsubcategory.Subcategory.ActivePromotion == true).OrderByDescending(p => p.UpdatedAt).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}
		public async Task<bool> StartPromotion(SaleDTO saleDTO)
		{
			var products = await _dbContext.Products.Include(p => p.Subsubcategory.Subcategory).Where(p => p.Subsubcategory.Subcategory.Id == saleDTO.CategoryId).ToListAsync();
			if(products.Count == 0)
			{
				return false;
			}
			foreach (var product in products)
			{
				product.OnSale = true;
				product.Discount = saleDTO.Discount;
				product.SalePrice = product.Price * (100 - saleDTO.Discount) / 100;
				product.DurationHour = saleDTO.Duration;
				product.SaleStartedAt = DateTime.Now;
				product.UpdatedAt = DateTime.Now;

				await _dbContext.SaveChangesAsync();
			}

			return true;
		}

		public async Task<bool> EndPromotion(Guid categoryId)
		{
			var products = await _dbContext.Products.Include(p => p.Subsubcategory.Subcategory).Where(p => p.Subsubcategory.Subcategory.Id == categoryId).ToListAsync();
			if (products.Count == 0)
			{
				return false;
			}
			foreach (var product in products)
			{
				product.OnSale = false;
				product.Discount = null;
				product.SalePrice = null;
				product.DurationHour = null;
				product.SaleStartedAt = null;

				await _dbContext.SaveChangesAsync();
			}

			return true;
		}

        public async Task<IEnumerable<ProductDTO>> SearchByCategory(string input)
        {
            return await _dbContext.Products.Include(p => p.Subsubcategory).Where(p => p.Subsubcategory.Name.ToLower().Contains(input)).OrderByDescending(p => p.UpdatedAt).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

		public async Task<IEnumerable<ProductDTO>> GetPromotions(int page)
		{
			int skip = (page - 1) * _size;
			return await _dbContext.Products.Include(p => p.Subsubcategory).Where(p => p.Subsubcategory.Subcategory.ActivePromotion == true).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

        public async Task<IEnumerable<ProductDTO>> GetBySubcategoryByPage(Guid subcategoryId, int page)
        {
            int skip = (page - 1) * _size;
            return await _dbContext.Products.Include(p => p.Subsubcategory).Where(p => p.Subsubcategory.Subcategory.Id == subcategoryId).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

		public async Task<IEnumerable<ProductDTO>> SortBy(int page, string option, string sortingOrder, string selection)
		{
            int skip = (page - 1) * _size;
			IEnumerable<ProductDTO> products = new List<ProductDTO>();
			if(selection == "all")
			{
				products = await _dbContext.Products.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
			}
			else
			{
				products = await _dbContext.Products.Where(p => p.OnSale == true).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
			}

			var sortedList = SortSwitch(products, page, option, sortingOrder);

			return sortedList;
        }

		private IEnumerable<ProductDTO> SortSwitch(IEnumerable<ProductDTO> products, int page, string option, string sortingOrder)
		{
			int skip = (page - 1) * _size;
			IEnumerable<ProductDTO> sortedList = new List<ProductDTO>();
			switch (option.ToLower())
			{
				case "title":
					if (sortingOrder == "desc")
					{
						sortedList = products.OrderByDescending(p => p.Title).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = products.OrderBy(p => p.Title).Skip(skip).Take(_size);
					}
					break;
				case "category":
					if (sortingOrder == "desc")
					{
						sortedList = products.OrderByDescending(p => p.Subsubcategory.Name).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = products.OrderBy(p => p.Subsubcategory.Name).Skip(skip).Take(_size);
					}
					break;
				case "price":
					if (sortingOrder == "desc")
					{
						sortedList = products.OrderByDescending(p => p.Price).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = products.OrderBy(p => p.Price).Skip(skip).Take(_size);
					}
					break;
				case "saleprice":
					if (sortingOrder == "desc")
					{
						sortedList = products.OrderByDescending(p => p.SalePrice).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = products.OrderBy(p => p.SalePrice).Skip(skip).Take(_size);
					}
					break;
				case "stock":
					if (sortingOrder == "desc")
					{
						sortedList = products.OrderByDescending(p => p.Stock).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = products.OrderBy(p => p.Stock).Skip(skip).Take(_size);
					}
					break;
				case "date":
					if (sortingOrder == "desc")
					{
						sortedList = products.OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = products.OrderBy(p => p.UpdatedAt).Skip(skip).Take(_size);
					}
					break;
				case "saledate":
					if (sortingOrder == "desc")
					{
						sortedList = products.OrderByDescending(p => p.SaleStartedAt).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = products.OrderBy(p => p.SaleStartedAt).Skip(skip).Take(_size);
					}
					break;
				case "duration":
					if (sortingOrder == "desc")
					{
						sortedList = products.OrderByDescending(p => p.DurationHour).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = products.OrderBy(p => p.DurationHour).Skip(skip).Take(_size);
					}
					break;
			}

			return sortedList;
		}
    }
}