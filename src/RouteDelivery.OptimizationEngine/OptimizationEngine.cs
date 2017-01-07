using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RouteDelivery.Data;
using RouteDelivery.Data.Implementations;

namespace RouteDelivery.OptimizationEngine
{
    public class OptimizationEngine: IOptimizationEngine
    {
        private Random _rnd = new Random();
        private IUnitOfWork _uof;

        public OptimizationEngine(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public void OptimizeDeliveries(int requestID)
        {
            var request = GetRequest(requestID);
            var customers = GetCustomers(request);
            var deliveries = GetDeliveries(request);
            var drivers = GetDrivers(request);

            var optimizedScheduled = new List<DeliverySchedule>();

            var deliveryNo = 0;

            foreach (var c in customers)
            {
                var customerDistanceFromWareHouse = GetCustomerDistanceFromWareHouse(c);
                deliveryNo = 0;

                foreach (var d in deliveries.Where(d => d.CustomerID == c.ID))
                {
                    var idealDriver = GetIdealDriver(drivers, d.TransportType, customerDistanceFromWareHouse);
                    deliveryNo++;

                    if (idealDriver != null)
                    {
                        optimizedScheduled.Add(new DeliverySchedule() { CustomerID = c.ID, DriverName = idealDriver.DriverName, OptimizationRequestID = request.ID, PackageID = d.ID, TransportType = d.TransportType, EstimatedTime = request.ScheduleDate.AddHours(deliveryNo), ID = deliveryNo});
                    }
                }
            }

            request.Status = RequestStatus.Complete;
            _uof.DeliverySchedule.AddRange(optimizedScheduled);
            _uof.SaveChanges();
        }

        #region Get Data Related To Request
        private IEnumerable<Driver> GetDrivers(object request)
        {
            return _uof.Drivers.FindAll();
        }

        private IEnumerable<Delivery> GetDeliveries(object request)
        {
            return _uof.Deliveries.FindAll();
        }

        private IEnumerable<Customer> GetCustomers(object request)
        {
            return _uof.Customers.FindAll();
        }

        private OptimizationRequest GetRequest(int requestID)
        {
            return _uof.OptimizationRequests.FindByID(requestID);
        }
        #endregion

        #region Optimize Delivery Helper Methods
        private Driver GetIdealDriver(IEnumerable<Driver> drivers, TransportType transportType, int customerDistanceFromWareHouse)
        {
            Thread.Sleep(500);
            return drivers.FirstOrDefault(d => d.TransportType == transportType && _rnd.Next(1, 4) == _rnd.Next(1, 4));
        }

        private int GetCustomerDistanceFromWareHouse(Customer c)
        {
            Thread.Sleep(500);
            return _rnd.Next(1, 100);
        }
        #endregion

    }
}
