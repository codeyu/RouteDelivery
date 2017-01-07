using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace RouteDelivery.Middleware
{
    public static class ErrorPage
    {
        public static async Task ResponseAsync(HttpResponse response, int statusCode)
        {
            if (statusCode == 404)
            {
                await response.WriteAsync(Page404);
            }
            else if (statusCode == 500)
            {
                await response.WriteAsync(Page500);
            }
        }

        private static string Page404 => @"not find";

        private static string Page500 => @"server error";
    }
}
