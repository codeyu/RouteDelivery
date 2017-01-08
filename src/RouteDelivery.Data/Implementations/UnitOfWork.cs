
using RouteDelivery.Models;

using RouteDriver.Data.Implementations;

namespace RouteDelivery.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<Customer> _customers;
        private IRepository<Delivery> _deliveries;
        private IRepository<Driver> _drivers;
        private IRepository<OptimizationRequest> _optimizationRequest;
        private IRepository<DeliverySchedule> _deliverySchedule;
        private RouteDeliveryDbContext _dbContext;
        public UnitOfWork() : this(RouteDeliveryDbContextFactory.Create())
        {
        }
        public UnitOfWork(RouteDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;

            _customers = new CustomerRepository(_dbContext);
            _deliveries = new DeliveryRepository(_dbContext);
            _drivers = new DriverRepository(_dbContext);
            _optimizationRequest = new OptimizationRequestRepository(_dbContext);
            _deliverySchedule = new DeliveryScheduleRepository(_dbContext);
        }

        public IRepository<Customer> Customers
        {
            get
            {
                return _customers;
            }
        }

        public IRepository<Delivery> Deliveries
        {
            get
            {
                return _deliveries;
            }
        }

        public IRepository<DeliverySchedule> DeliverySchedule
        {
            get
            {
                return _deliverySchedule;
            }
        }

        public IRepository<Driver> Drivers
        {
            get
            {
                return _drivers;
            }
        }

        public IRepository<OptimizationRequest> OptimizationRequests
        {
            get
            {
                return _optimizationRequest;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}