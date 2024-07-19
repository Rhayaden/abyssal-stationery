using AutoMapper;
using Blazor.Server.Models;
using Blazor.Shared.DTOs;

namespace Blazor.Server.Mappings
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<User, UserDTO>().ReverseMap();
			CreateMap<Cart, CartDTO>().ReverseMap();
			CreateMap<Admin, AdminDTO>().ReverseMap();
			CreateMap<Category, CategoryDTO>().ReverseMap();
			CreateMap<Subcategory, SubcategoryDTO>().ReverseMap();
			CreateMap<Sub_subcategory, Sub_subcategoryDTO>().ReverseMap();
			CreateMap<Product, ProductDTO>().ReverseMap();
			CreateMap<ProductInCart, ProductInCartDTO>().ReverseMap();
			CreateMap<ProductInOrder, ProductInOrderDTO>().ReverseMap();
			CreateMap<Order, OrderDTO>().ReverseMap();
		}
	}
}
