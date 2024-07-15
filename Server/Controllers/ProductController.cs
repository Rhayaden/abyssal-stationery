using Blazor.Server.Services;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        [HttpGet("Search/{input}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> Search(string input)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.Search(input),
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
		[HttpGet("GetPage/{pageNumber}")]
		public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetByPage(int pageNumber)
        {

            return new ServiceResponse<IEnumerable<ProductDTO>>()
			{
				Data = await _productService.GetByPage(pageNumber),
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

        [HttpGet("Get/Category/{categoryId}")]
        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetByCategory(Guid categoryId)
        {
            return new ServiceResponse<IEnumerable<ProductDTO>>()
            {
                Data = await _productService.GetByCategory(categoryId),
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
