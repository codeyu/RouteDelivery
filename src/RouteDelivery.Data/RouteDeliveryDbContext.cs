using Microsoft.EntityFrameworkCore;
using RouteDelivery.Models;

namespace RouteDelivery.Data
{
    public class RouteDeliveryDbContext : DbContext
    {
            
        public RouteDeliveryDbContext(DbContextOptions<RouteDeliveryDbContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Delivery> Deliveries { get; set; }
        public virtual DbSet<DeliverySchedule> DeliverySchedules { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<OptimizationRequest> OptimizationRequests { get; set; }
    }
}