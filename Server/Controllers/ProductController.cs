using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
			_productService = productService;
        }
		[HttpGet("Count")]
		public async Task<ServiceResponse<int>> Count()
        {
            return new ServiceResponse<int>()
            {
                Data = await _productService.Count(),
            };
        }
		[HttpGet("CountOnSales")]
		public async Task<ServiceResponse<int>> CountOnSales()
		{
			return new ServiceResponse<int>()
			{
				Data = await _productService.CountOnSales(),
			};
		}
        [HttpGet("CountBy/MainCategory/{categoryId}")]
        public async Task<ServiceResponse<int>> CountByMainCategory(Guid categoryId)
        {
            return new ServiceResponse<int>()
            {
                Data = await _productService.CountByMainCategory(categoryId),
            };
        }
        [HttpGet("CountBy/Subcategory/{subcategoryId}")]
        public async Task<ServiceResponse<int>> CountBySubcategory(Guid subcategoryId)
        {
            return new ServiceResponse<int>()
            {
                Data = await _productService.CountBySubcategory(subcategoryId),
            };
        }
        [HttpGet("CountBy/Sub_subcategory/{subSubcategoryId}")]
        public async Task<ServiceResponse<int>> CountBySubsubcategory(Guid subSubcategoryId)
        {
            return new ServiceResponse<int>()
            {
                Data = await _productService.CountBySubsubcategory(subSubcategoryId),
            };
        }
        [HttpPost("CheckSale")]
        public async Task<ServiceResponse<bool>> CheckSale()
        {
            return new ServiceResponse<bool>()
            {
                Data = await _productService.CheckSale(),
            };
        }
		[HttpPost("Sale/Start")]
		public async Task<ServiceResponse<bool>> StartSaleByCategory([FromBody] SaleDTO saleDTO)
		{
			return new ServiceResponse<bool>()
			{
				Data = await _productService.StartPromotion(saleDTO),
			};
		}
		[HttpPost("Sale/End/{categoryId}")]
		public async Task<ServiceResponse<bool>> EndSaleByCategory(Guid categoryId)
		{
			return new ServiceResponse<bool>()
			{
				Data = await _productService.EndPromotion(categoryId),
			};
		}
		[HttpGet("Search/{input}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> Search(string input)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.Search(input),
            };
        }
        [HttpGet("SearchByCategory/{input}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> SearchByCategory(string input)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.SearchByCategory(input),
            };
        }
        [HttpGet("SortBy/{option}/{sortingOrder}/{selection}/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> SortBy(int pageNumber, string option, string sortingOrder, string selection)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.SortBy(pageNumber, option, sortingOrder, selection),
            };
        }
        [HttpGet("Get")]
		public async Task<ServiceResponse<IEnumerable<ProductDTO>>> Get()
		{
			return new ServiceResponse<IEnumerable<ProductDTO>>()
			{
				Data = await _productService.Get(),
			};
		}
		[HttpGet("GetAllSales")]
		public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetAllSales()
		{
			return new ServiceResponse<IEnumerable<ProductDTO>>()
			{
				Data = await _productService.GetAllSales(),
			};
		}
		[HttpGet("GetAllPromotions")]
		public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetAllPromotions()
		{
			return new ServiceResponse<IEnumerable<ProductDTO>>()
			{
				Data = await _productService.GetAllPromotions(),
			};
		}
		[HttpGet("GetPage/{pageNumber}")]
		public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetByPage(int pageNumber)
        {

            return new ServiceResponse<IEnumerable<ProductDTO>>()
			{
				Data = await _productService.GetByPage(pageNumber),
			};
		}
        [HttpGet("GetOnSales/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetOnSales(int pageNumber)
        {

            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.GetOnSales(pageNumber),
            };
        }
		[HttpGet("GetPromotions/{pageNumber}")]
		public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetPromotions(int pageNumber)
		{

			return new ServiceResponse<IEnumerable<ProductDTO>>()
			{
				Data = await _productService.GetPromotions(pageNumber),
			};
		}
		[HttpGet("Get/{productId}")]
		public async Task<ServiceResponse<ProductDTO>> GetById(Guid productId)
		{
			return new ServiceResponse<ProductDTO>()
			{
				Data = await _productService.GetByID(productId),
			};
		}

        [HttpGet("Get/Subcategory/{subcategoryId}/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetBySubcategory(Guid subcategoryId, int pageNumber)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.GetBySubcategory(subcategoryId, pageNumber),
            };
        }
        [HttpGet("Get/{subcategoryId}/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetBySubcategoryByPage(Guid subcategoryId, int pageNumber)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.GetBySubcategoryByPage(subcategoryId, pageNumber),
            };
        }
        [HttpGet("Get/MainCategory/{categoryId}/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetByMainCategory(Guid categoryId, int pageNumber)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.GetByMainCategory(categoryId, pageNumber),
            };
        }
        [HttpGet("Get/Subsubcategory/{subSubcategoryId}/{pageNumber}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetBySubsubcategory(Guid subSubcategoryId, int pageNumber)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.GetBySubsubcategory(subSubcategoryId, pageNumber),
            };
        }

        [Authorize]
		[HttpPost("Create")]
		public async Task<ServiceResponse<ProductDTO>> Create([FromBody] ProductDTO productDTO)
		{
			return new ServiceResponse<ProductDTO>()
			{
				Data = await _productService.Create(productDTO)
			};
		}
		[Authorize]
		[HttpPost("Update")]
		public async Task<ServiceResponse<ProductDTO>> Update([FromBody] ProductDTO productDTO)
		{
			return new ServiceResponse<ProductDTO>()
			{
				Data = await _productService.Update(productDTO)
			};
		}
		[Authorize]
		[HttpDelete("Delete/{productId}")]
		public async Task<ServiceResponse<ProductDTO>> Delete(Guid productId)
		{
			var deletedProduct = await _productService.GetByID(productId);
			if (deletedProduct != null)
			{
				await _productService.Delete(productId);
			}
			return new ServiceResponse<ProductDTO>()
			{
				Data = deletedProduct
			};
		}
	}
}
