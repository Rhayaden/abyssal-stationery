namespace Blazor.Server.Services.IServices
{
	public interface IImageService
	{
		Task<string> UploadAsync(IFormFile formFile);
	}
}
