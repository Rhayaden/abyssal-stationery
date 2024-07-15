using Blazor.Shared.ResponseModels;
using Blazor.Shared.Utilities;
using System.Net.Http.Json;

namespace Blazor.Client.Utilities
{
	public static class HttpClientExtensions
	{
		public async static Task<T> GetServiceResponseAsync<T>(this HttpClient client, string url)
		{
			var response = await client.GetFromJsonAsync<ServiceResponse<T>>(url);
			if (!response.IsSuccess)
			{
				throw new AppException(response.Message);
			}

			return response.Data;
		}
		public async static Task<TData> PostServiceResponseAsync<TData, TValue>(this HttpClient client, string url, TValue value)
		{
			var responseMessage = await client.PostAsJsonAsync(url, value);

			if (responseMessage.IsSuccessStatusCode)
			{
				var response = await responseMessage.Content.ReadFromJsonAsync<ServiceResponse<TData>>();

				return !response.IsSuccess ? throw new AppException(response.Message) : response.Data;
			}

		throw new Exception(responseMessage.StatusCode.ToString());
	}
	}
}
