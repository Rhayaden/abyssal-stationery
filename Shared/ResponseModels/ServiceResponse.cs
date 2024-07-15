namespace Blazor.Shared.ResponseModels
{
	public class ServiceResponse<T> : BaseResponse
	{
		public T Data { get; set; }

	}
}
