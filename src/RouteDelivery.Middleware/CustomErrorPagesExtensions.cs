using System;
using Microsoft.AspNetCore.Builder;

namespace RouteDelivery.Middleware
{
    public static class CustomErrorPagesExtensions
    {
        public static IApplicationBuilder UseCustomErrorPages(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<CustomErrorPagesMiddleware>();
        }
    }
}
