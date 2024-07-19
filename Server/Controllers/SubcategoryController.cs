﻿using Blazor.Server.Services;
using Blazor.Server.Services.IServices;
using Blazor.Shared.DTOs;
using Blazor.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class SubcategoryController : ControllerBase
	{
		private readonly ISubcategoryService _subCategoryService;

		public SubcategoryController(ISubcategoryService subCategoryService)
        {
			_subCategoryService = subCategoryService;
		}
		[HttpGet("Count")]
		public async Task<ServiceResponse<int>> Count()
		{
			return new ServiceResponse<int>()
			{
				Data = await _subCategoryService.Count(),
			};
		}
		[HttpGet("Search/{input}")]
		public async Task<ServiceResponse<IEnumerable<SubcategoryDTO>>> Search(string input)
		{
			return new ServiceResponse<IEnumerable<SubcategoryDTO>>()
			{
				Data = await _subCategoryService.Search(input),
			};
		}
        [HttpGet("Promotions")]
        public async Task<ServiceResponse<IEnumerable<SubcategoryDTO>>> GetPromotions()
        {
            return new ServiceResponse<IEnumerable<SubcategoryDTO>>()
            {
                Data = await _subCategoryService.GetPromotions(),
            };
        }
        [HttpGet("Get")]
		public async Task<ServiceResponse<IEnumerable<SubcategoryDTO>>> Get()
		{
			return new ServiceResponse<IEnumerable<SubcategoryDTO>>()
			{
				Data = await _subCategoryService.Get(),
			};
		}
		[HttpGet("Get/Page/{pageNumber}")]
		public async Task<ServiceResponse<IEnumerable<SubcategoryDTO>>> GetByPage(int pageNumber)
		{
			return new ServiceResponse<IEnumerable<SubcategoryDTO>>()
			{
				Data = await _subCategoryService.GetByPage(pageNumber),
			};
		}
		[HttpGet("Get/{subcategoryId}")]
		public async Task<ServiceResponse<SubcategoryDTO>> GetById(Guid subcategoryId)
		{
			return new ServiceResponse<SubcategoryDTO>()
			{
				Data = await _subCategoryService.GetByID(subcategoryId),
			};
		}
		[Authorize]
		[HttpPost("Create")]
		public async Task<ServiceResponse<SubcategoryDTO>> Create([FromBody] SubcategoryDTO subcategoryDTO)
		{
			return new ServiceResponse<SubcategoryDTO>()
			{
				Data = await _subCategoryService.Create(subcategoryDTO)
			};
		}
		[Authorize]
		[HttpPost("Update")]
		public async Task<ServiceResponse<SubcategoryDTO>> Update([FromBody] SubcategoryDTO subcategoryDTO)
		{
			return new ServiceResponse<SubcategoryDTO>()
			{
				Data = await _subCategoryService.Update(subcategoryDTO)
			};
		}
		[Authorize]
		[HttpDelete("Delete/{subcategoryId}")]
		public async Task<ServiceResponse<SubcategoryDTO>> Delete(Guid subcategoryId)
		{
			var deletedCategory = await _subCategoryService.GetByID(subcategoryId);
			if (deletedCategory != null)
			{
				await _subCategoryService.Delete(subcategoryId);
			}
			return new ServiceResponse<SubcategoryDTO>()
			{
				Data = deletedCategory
			};
		}
	}
}