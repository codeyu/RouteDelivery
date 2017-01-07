
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
namespace RouteDelivery.Middleware
{
    public class CustomErrorPagesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomErrorPagesMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<CustomErrorPagesMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "An unhandled exception has occurred while executing the request");

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error page middleware will not be executed.");
                    throw;
                }
                try
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 500;
                    return;
                }
                catch (Exception ex2)
                {
                    _logger.LogError(0, ex2, "An exception was thrown attempting to display the error page.");
                }
                throw;
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                if (statusCode == 404 || statusCode == 500)
                {
                    await ErrorPage.ResponseAsync(context.Response, statusCode);
                }
            }
        }
    }
}
