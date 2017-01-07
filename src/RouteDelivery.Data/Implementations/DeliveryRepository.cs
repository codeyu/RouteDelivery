using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteDelivery.Data.Implementations
{
    public class DeliveryRepository : IRepository<Delivery>
    {
        private RouteDeliveryDbContext _dbContext;

        public DeliveryRepository(RouteDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
            if (_dbContext.Deliveries.Count() == 0)
            {
                AddRange(GetMockDeliveryData());
                _dbContext.SaveChanges();
            }
        }

        private List<Delivery> GetMockDeliveryData()
        {
            return new List<Delivery>() {
                new Delivery() {CustomerID = 1, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 1, TransportType = TransportType.Light},
                new Delivery() {CustomerID = 2, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 3, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 3, TransportType = TransportType.Light},
                new Delivery() {CustomerID = 3, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 4, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 4, TransportType = TransportType.Light},
                new Delivery() {CustomerID = 5, TransportType = TransportType.Light},
                new Delivery() {CustomerID = 5, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 6, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 6, TransportType = TransportType.Light},
                new Delivery() {CustomerID = 7, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 7, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 8, TransportType = TransportType.Heavy},
                new Delivery() {CustomerID = 8, TransportType = TransportType.Light}
            };
        }

        public void Add(Delivery newEntity)
        {
            _dbContext.Deliveries.Add(newEntity);
        }

        public List<Delivery> Find(Func<Delivery, bool> match)
        {
            return _dbContext.Deliveries.Where(match).ToList();
        }

        public List<Delivery> FindAll()
        {
            return _dbContext.Deliveries.ToList();
        }

        public void Remove(Delivery entity)
        {
            _dbContext.Deliveries.Remove(entity);
        }

        public void Update(Delivery entity)
        {

        }

        public Delivery FindByID(int id)
        {
            var results = _dbContext.Deliveries.Where(d => d.ID == id);

            return results.FirstOrDefault();
        }

        public void AddRange(List<Delivery> deliveries)
        {
            _dbContext.Deliveries.AddRange(deliveries);
        }
    }
}