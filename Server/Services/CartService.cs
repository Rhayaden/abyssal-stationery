using AutoMapper;
using Blazor.Server.Data;
using Blazor.Server.Models;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
{
	public class CartService : ICartService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
		public CartService(IMapper mapper, AppDbContext dbContext)
        {
			_mapper = mapper;
			_dbContext = dbContext;
		}
        public async Task<CartDTO> Add(Guid userId, ProductInCartDTO productDTO, int quantity = 1)
		{
			if (productDTO.Stock < 1)
			{
				//throw new AppException("Out of stock");
				return new CartDTO
				{
					Id = Guid.Empty,
				};
			}
			var cart = await _dbContext.Carts.Include("Products").FirstOrDefaultAsync(c => c.UserId.ToString() == userId.ToString());
			if (cart != null)
			{
				var existingProduct = cart.Products.FirstOrDefault(p => p.ProductId == productDTO.ProductId);
				var product = _mapper.Map<ProductInCart>(productDTO);
				if (existingProduct is null)
				{
					productDTO.Quantity = productDTO.Quantity + quantity;
					cart.Quantity = cart.Quantity + quantity;
					cart.Total = cart.Total + productDTO.Price * quantity;
					cart.Products.Add(product);
					await _dbContext.SaveChangesAsync();
				}
				else
				{
					var existingProductDTO = _mapper.Map<ProductInCartDTO>(existingProduct);
					existingProductDTO.Quantity += quantity;
					cart.Quantity = cart.Quantity + quantity;
					cart.Total = cart.Total + productDTO.Price * quantity;
					await _dbContext.SaveChangesAsync();
				}

				var productsInCart = await _dbContext.ProductsInCarts.FirstOrDefaultAsync(p => p.ProductId == productDTO.ProductId && p.CartId == cart.Id);

				if (productsInCart is null)
				{
					await _dbContext.ProductsInCarts.AddAsync(product);
					await _dbContext.SaveChangesAsync();
				}
				else
				{
					productsInCart.Quantity = productsInCart.Quantity + quantity;
					await _dbContext.SaveChangesAsync();
				}
			}
			return _mapper.Map<CartDTO>(cart);
		}

		public async Task<CartDTO> Clear(Guid userId)
		{
			var cart = await _dbContext.Carts.Include("Products").FirstOrDefaultAsync(c => c.UserId == userId);
			if (cart != null)
			{
				cart.Products = null;
				cart.Quantity = 0;
				cart.Total = 0;
				await _dbContext.SaveChangesAsync();
			}
			var productsInCarts = await _dbContext.ProductsInCarts.Where(p => p.CartId.ToString() == null).ToListAsync();
			foreach (var product in productsInCarts)
			{
				_dbContext.ProductsInCarts.Remove(product);
				await _dbContext.SaveChangesAsync();
			}
			return _mapper.Map<CartDTO>(cart);
		}

		public async Task<CartDTO> Get(Guid userId)
		{
			var cart = await _dbContext.Carts.Include("Products").FirstOrDefaultAsync(c => c.UserId == userId);
			if (cart == null)
			{
				throw new AppException("Cart not found");
			}
			return _mapper.Map<CartDTO>(cart);
		}

		public async Task<CartDTO> Remove(Guid userId, ProductInCartDTO productDTO, int quantity = 1)
		{
			var cart = await _dbContext.Carts.Include("Products").FirstOrDefaultAsync(c => c.UserId == userId);
			if (cart != null)
			{
				var productsInCarts = await _dbContext.ProductsInCarts.FirstOrDefaultAsync(p => p.CartId == cart.Id && p.ProductId == productDTO.ProductId);
				var product = _mapper.Map<ProductInCart>(productDTO);
				cart.Quantity = cart.Quantity - productDTO.Quantity;
				cart.Total = cart.Total - productDTO.Price * productDTO.Quantity;
				cart.Products.Remove(product);
				await _dbContext.SaveChangesAsync();

				_dbContext.ProductsInCarts.Remove(productsInCarts);
				await _dbContext.SaveChangesAsync();
			}
			return _mapper.Map<CartDTO>(cart);
		}
	}
}
