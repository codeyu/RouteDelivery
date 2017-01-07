using RouteDelivery.Data;
using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteDriver.Data.Implementations
{
    public class DriverRepository : IRepository<Driver>
    {
        private RouteDeliveryDbContext _dbContext;

        public DriverRepository(RouteDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
            if (_dbContext.Drivers.Count() == 0)
            {
                AddRange(GetMockDriverData());
                _dbContext.SaveChanges();
            }
        }

        private List<Driver> GetMockDriverData()
        {
            return new List<Driver>() {
                new Driver() {ID = 1, DriverName = "M Shoemacker", StartLocation = "Y432 1234", TransportType = TransportType.Heavy, StartTime = new DateTime(1900,01,01,6,0,0), EndTime = new DateTime(1900,01,01,17,0,0)},
                new Driver() {ID = 2, DriverName = "A Frost", StartLocation = "Y432 5678", TransportType = TransportType.Light, StartTime = new DateTime(1900,01,01,8,0,0), EndTime = new DateTime(1900,01,01,17,0,0)},
                new Driver() {ID = 3, DriverName = "A McKenna", StartLocation = "Y432 9123", TransportType = TransportType.Heavy, StartTime = new DateTime(1900,01,01,12,0,0), EndTime = new DateTime(1900,01,21,6,0,0)},
                new Driver() {ID = 4 ,DriverName = "N Hamilton", StartLocation = "Y432 8567", TransportType = TransportType.Light, StartTime = new DateTime(1900,01,01,6,30,0), EndTime = new DateTime(1900,01,01,17,0,0)},
                new Driver() {ID = 5, DriverName = "M Takkinen", StartLocation = "Y432 1234", TransportType = TransportType.Heavy, StartTime = new DateTime(1900,01,01,6,0,0), EndTime = new DateTime(1900,01,01,17,0,0)},
                new Driver() {ID = 6, DriverName = "A Ralonso", StartLocation = "Y432 5678", TransportType = TransportType.Light, StartTime = new DateTime(1900,01,01,8,0,0), EndTime = new DateTime(1900,01,01,17,0,0)},
                new Driver() {ID = 7, DriverName = "A Nasssa", StartLocation = "Y432 9123", TransportType = TransportType.Heavy, StartTime = new DateTime(1900,01,01,12,0,0), EndTime = new DateTime(1900,01,21,6,0,0)},
                new Driver() {ID = 8 ,DriverName = "N Canselle", StartLocation = "Y432 8567", TransportType = TransportType.Light, StartTime = new DateTime(1900,01,01,6,30,0), EndTime = new DateTime(1900,01,01,17,0,0)}
            };
        }

        public void Add(Driver newEntity)
        {
            _dbContext.Drivers.Add(newEntity);
        }

        public List<Driver> Find(Func<Driver, bool> match)
        {
            return _dbContext.Drivers.Where(match).ToList();
        }

        public List<Driver> FindAll()
        {
            return _dbContext.Drivers.ToList();
        }

        public void Remove(Driver entity)
        {
            _dbContext.Drivers.Remove(entity);
        }

        public void Update(Driver entity)
        {
        }

        public Driver FindByID(int id)
        {
            var results = _dbContext.Drivers.Where(d => d.ID == id);

            return results.FirstOrDefault();
        }

        public void AddRange(List<Driver> Drivers)
        {
            _dbContext.Drivers.AddRange(Drivers);
        }
    }
}