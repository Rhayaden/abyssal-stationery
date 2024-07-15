using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
        {
			_categoryService = categoryService;
		}
        [HttpGet("Count")]
        public async Task<ServiceResponse<int>> Count()
        {
            return new ServiceResponse<int>()
            {
                Data = await _categoryService.Count(),
            };
        }
        [HttpGet("Get")]
		public async Task<ServiceResponse<IEnumerable<CategoryDTO>>> Get()
		{
			return new ServiceResponse<IEnumerable<CategoryDTO>>()
			{
				Data = await _categoryService.Get(),
			};
		}
		[HttpGet("Get/Page/{pageNumber}")]
		public async Task<ServiceResponse<IEnumerable<CategoryDTO>>> GetByPage(int pageNumber)
		{
			return new ServiceResponse<IEnumerable<CategoryDTO>>()
			{
				Data = await _categoryService.GetByPage(pageNumber),
			};
		}
		[HttpGet("Get/{categoryId}")]
		public async Task<ServiceResponse<CategoryDTO>> GetById(Guid categoryId)
		{
			return new ServiceResponse<CategoryDTO>()
			{
				Data = await _categoryService.GetByID(categoryId),
			};
		}

		[Authorize]
		[HttpPost("Create")]
		public async Task<ServiceResponse<CategoryDTO>> Create([FromBody] CategoryDTO categoryDTO)
		{
			return new ServiceResponse<CategoryDTO>()
			{
				Data = await _categoryService.Create(categoryDTO)
			};
		}
		[Authorize]
		[HttpPost("Update")]
		public async Task<ServiceResponse<CategoryDTO>> Update([FromBody] CategoryDTO categoryDTO)
		{
			return new ServiceResponse<CategoryDTO>()
			{
				Data = await _categoryService.Update(categoryDTO)
			};
		}
		[Authorize]
		[HttpDelete("Delete/{categoryId}")]
		public async Task<ServiceResponse<CategoryDTO>> Delete(Guid categoryId)
		{
			var deletedCategory = await _categoryService.GetByID(categoryId);
			if (deletedCategory != null)
			{
				await _categoryService.Delete(categoryId);
			}
			return new ServiceResponse<CategoryDTO>()
			{
				Data = deletedCategory
			};
		}
	}
}
