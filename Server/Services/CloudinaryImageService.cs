using Blazor.Server.Services.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Blazor.Server.Services
{
	public class CloudinaryImageService : IImageService
	{
		private readonly IConfiguration _configuration;
		private readonly Account _account;

		public CloudinaryImageService(IConfiguration configuration)
		{
			_configuration = configuration;
			_account = new Account(
				configuration.GetSection("Cloudinary")["CloudName"],
				configuration.GetSection("Cloudinary")["ApiKey"],
				configuration.GetSection("Cloudinary")["ApiSecret"]
				);
		}
		public async Task<string> UploadAsync(IFormFile formFile)
		{
			var client = new Cloudinary(_account);

			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(formFile.FileName, formFile.OpenReadStream()),
				DisplayName = formFile.FileName,
			};

			var uploadResult = await client.UploadAsync(uploadParams);

			if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return uploadResult.SecureUrl.ToString();
			}
			return null;
		}
	}
}
