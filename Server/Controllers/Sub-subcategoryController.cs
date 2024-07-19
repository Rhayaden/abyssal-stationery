using Blazor.Server.Services;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Sub_subcategoryController : ControllerBase
	{
		private readonly ISub_subcategoryService _sub_SubcategoryService;

		public Sub_subcategoryController(ISub_subcategoryService sub_SubcategoryService)
        {
			_sub_SubcategoryService = sub_SubcategoryService;
		}
		[HttpGet("Count")]
		public async Task<ServiceResponse<int>> Count()
		{
			return new ServiceResponse<int>()
			{
				Data = await _sub_SubcategoryService.Count(),
			};
		}
		[HttpGet("Search/{input}")]
		public async Task<ServiceResponse<IEnumerable<Sub_subcategoryDTO>>> Search(string input)
		{
			return new ServiceResponse<IEnumerable<Sub_subcategoryDTO>>()
			{
				Data = await _sub_SubcategoryService.Search(input),
			};
		}
		[HttpGet("Get")]
		public async Task<ServiceResponse<IEnumerable<Sub_subcategoryDTO>>> Get()
		{
			return new ServiceResponse<IEnumerable<Sub_subcategoryDTO>>()
			{
				Data = await _sub_SubcategoryService.Get(),
			};
		}
		[HttpGet("Get/Page/{pageNumber}")]
		public async Task<ServiceResponse<IEnumerable<Sub_subcategoryDTO>>> GetByPage(int pageNumber)
		{
			return new ServiceResponse<IEnumerable<Sub_subcategoryDTO>>()
			{
				Data = await _sub_SubcategoryService.GetByPage(pageNumber),
			};
		}
		[HttpGet("Get/{subcategoryId}")]
		public async Task<ServiceResponse<Sub_subcategoryDTO>> GetById(Guid subcategoryId)
		{
			return new ServiceResponse<Sub_subcategoryDTO>()
			{
				Data = await _sub_SubcategoryService.GetByID(subcategoryId),
			};
		}
		[Authorize]
		[HttpPost("Create")]
		public async Task<ServiceResponse<Sub_subcategoryDTO>> Create([FromBody] Sub_subcategoryDTO subSubcategoryDTO)
		{
			return new ServiceResponse<Sub_subcategoryDTO>()
			{
				Data = await _sub_SubcategoryService.Create(subSubcategoryDTO)
			};
		}
		[Authorize]
		[HttpPost("Update")]
		public async Task<ServiceResponse<Sub_subcategoryDTO>> Update([FromBody] Sub_subcategoryDTO subSubcategoryDTO)
		{
			return new ServiceResponse<Sub_subcategoryDTO>()
			{
				Data = await _sub_SubcategoryService.Update(subSubcategoryDTO)
			};
		}
		[Authorize]
		[HttpDelete("Delete/{subSubcategoryId}")]
		public async Task<ServiceResponse<Sub_subcategoryDTO>> Delete(Guid subSubcategoryId)
		{
			var deletedCategory = await _sub_SubcategoryService.GetByID(subSubcategoryId);
			if (deletedCategory != null)
			{
				await _sub_SubcategoryService.Delete(subSubcategoryId);
			}
			return new ServiceResponse<Sub_subcategoryDTO>()
			{
				Data = deletedCategory
			};
		}
	}
}
