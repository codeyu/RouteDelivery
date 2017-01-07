using RouteDelivery.Models;


namespace RouteDelivery.Data
{
    public interface IUnitOfWork
    {
        IRepository<Customer> Customers { get; }
        IRepository<Delivery> Deliveries { get; }
        IRepository<Driver> Drivers { get; }
        IRepository<OptimizationRequest> OptimizationRequests { get; }
        IRepository<DeliverySchedule> DeliverySchedule { get; }

        void SaveChanges();
    }
}