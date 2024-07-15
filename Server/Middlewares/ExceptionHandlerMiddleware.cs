using Blazor.Shared.Utilities;

namespace Blazor.Server.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly ILogger _logger;
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
			RequestDelegate next)
		{
			_logger = logger;
			_next = next;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (AppException ex)
			{
				await httpContext.Response.WriteAsJsonAsync(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);

				await httpContext.Response.WriteAsync(ex.Message);
			}
		}
	}
}
