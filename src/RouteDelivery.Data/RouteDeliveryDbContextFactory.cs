
using Microsoft.EntityFrameworkCore;
namespace RouteDelivery.Data
{
    public class RouteDeliveryDbContextFactory
    {
        public static RouteDeliveryDbContext Create()
        {
            
            var optionsBuilder = new DbContextOptionsBuilder<RouteDeliveryDbContext>();
            optionsBuilder.UseSqlite("Data Source=RouteDelivery.db");

            return new RouteDeliveryDbContext(optionsBuilder.Options);
        }
    }
}