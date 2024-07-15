using Blazor.Server.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private readonly IImageService _imageService;

		public ImageController(IImageService imageService)
        {
			_imageService = imageService;
		}
		[HttpPost]
		public async Task<IActionResult> UploadAsync(IFormFile formFile)
		{
			var imageUrl = await _imageService.UploadAsync(formFile);

			if (imageUrl == null)
			{
				return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
			}

			return new JsonResult(new { link = imageUrl });
		}
	}
}
