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
    public class OrderService : IOrderService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _dbContext;
        private readonly int _size;
        public OrderService(IMapper mapper, AppDbContext dbContext)
        {
			_mapper = mapper;
            _dbContext = dbContext;
            _size = ItemsPerPage.Order;
		}
        public async Task<int> Count()
        {
            return await _dbContext.Orders.CountAsync();
        }

        public async Task<int> CountByUser(Guid userId)
        {
            return await _dbContext.Orders.Where(o => o.UserId == userId).CountAsync();
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(Guid userId)
		{
			return await _dbContext.Orders.Include("Products").Where(o => o.UserId == userId).OrderByDescending(o => o.CreateDate).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToListAsync();
		}

        public async Task<IEnumerable<OrderDTO>> GetAllByPage(Guid userId, int page)
        {
            int skip = (page - 1) * _size;
            return await _dbContext.Orders.Include("Products").Where(o => o.UserId == userId).OrderByDescending(o => o.CreateDate).Skip(skip).Take(_size).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<OrderDTO> GetByID(Guid orderId)
		{
			var order = await _dbContext.Orders.Include("Products").FirstOrDefaultAsync(o => o.Id == orderId);

			return _mapper.Map<OrderDTO>(order);
		}

		public async Task<OrderDTO> Order(CartDTO cartDTO)
		{
			var cartProducts = cartDTO.Products;

			var products = await _dbContext.Products.ToListAsync();
			foreach (var product in cartProducts)
			{
				var productInCart = products.FirstOrDefault(p => p.Id == product.ProductId);
				if (productInCart?.Stock < 1)
				{
					//throw new AppException("Out of stock");
					return null;
				}
			}

			List<ProductInOrderDTO> orderProducts = new();
			foreach (var cartProduct in cartProducts)
			{
				orderProducts.Add(new ProductInOrderDTO()
				{
					ProductId = cartProduct.Id,
					ProductTitle = cartProduct.ProductTitle,
					Category = cartProduct.Category,
					Image = cartProduct.Image,
					Price = cartProduct.Price,
					Quantity = cartProduct.Quantity,
					CartId = cartDTO.Id
				});
			}

			var orderDTO = new OrderDTO()
			{
				UserId = cartDTO.UserId,
				Products = orderProducts,
				Total = cartDTO.Total,
			};

			var checkout = _mapper.Map<Order>(orderDTO);

			await _dbContext.Orders.AddAsync(checkout);
			await _dbContext.SaveChangesAsync();

			var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == cartDTO.UserId);
			user.Orders.Add(checkout);

			await _dbContext.SaveChangesAsync();

			return orderDTO;
		}

		public async Task<IEnumerable<OrderDTO>> SortBy(int page, string option, string sortingOrder)
		{
			int skip = (page - 1) * _size;
			var orders = await _dbContext.Orders.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToListAsync();
			IEnumerable<OrderDTO> sortedList = new List<OrderDTO>();
			switch (option.ToLower())
			{
				case "total":
					if (sortingOrder == "desc")
					{
						sortedList = orders.OrderByDescending(o => o.Total).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = orders.OrderBy(o => o.Total).Skip(skip).Take(_size);
					}
					break;
				case "date":
					if (sortingOrder == "desc")
					{
						sortedList = orders.OrderByDescending(o => o.CreateDate).Skip(skip).Take(_size);
					}
					else
					{
						sortedList = orders.OrderBy(o => o.CreateDate).Skip(skip).Take(_size);
					}
					break;
			}

			return sortedList;
		}
	}
}
