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
			return await _dbContext.Products.Include(p => p.Category).OrderByDescending(p => p.UpdatedAt).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<IEnumerable<ProductDTO>> GetByPage(int page)
		{
			int skip = (page - 1) * _size;
			return await _dbContext.Products.Include(p => p.Category).OrderByDescending(p => p.UpdatedAt).Skip(skip).Take(_size).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

		public async Task<IEnumerable<ProductDTO>> GetByCategory(Guid categoryId)
        {
            return await _dbContext.Products.Where(p => p.CategoryId == categoryId).OrderByDescending(p => p.UpdatedAt).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).ToListAsync();
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
			product.CategoryId = productDTO.CategoryId;
			product.Price = productDTO.Price;
			if(product.Stock == productDTO.Stock)
			{
				product.UpdatedAt = DateTime.Now;
			}
			product.Stock = productDTO.Stock;

			await _dbContext.SaveChangesAsync();

			return _mapper.Map<ProductDTO>(product);
		}
	}
}
