using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteDelivery.Data.Implementations
{
    public class CustomerRepository : IRepository<Customer>
    {
        private RouteDeliveryDbContext _dbContext;

        public CustomerRepository(RouteDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
            if (_dbContext.Customers.Count() == 0)
            {
                AddRange(GetMockCustomerData());
                _dbContext.SaveChanges();
            }
        }

        private List<Customer> GetMockCustomerData()
        {
            return new List<Customer>() {
                new Customer() {ID = 1, CustomerName = "Smith", CustomerLocation = "Z324 4323"},
                new Customer() {ID = 2, CustomerName = "Singh", CustomerLocation = "Y434 5434"},
                new Customer() {ID = 3, CustomerName = "Jones", CustomerLocation = "Y432 2341"},
                new Customer() {ID = 4, CustomerName = "Patel", CustomerLocation = "Z223 3423"},
                new Customer() {ID = 5, CustomerName = "O’Brien", CustomerLocation = "Z453 5645"},
                new Customer() {ID = 6, CustomerName = "Kumar", CustomerLocation = "Z234 3856"},
                new Customer() {ID = 7, CustomerName = "Rogers", CustomerLocation = "Z324 6576"},
                new Customer() {ID = 8, CustomerName = "Devi", CustomerLocation = "Z111 2132"}
            };
        }

        public void Add(Customer newEntity)
        {
            _dbContext.Customers.Add(newEntity);
        }

        public List<Customer> Find(Func<Customer, bool> match)
        {
            return _dbContext.Customers.Where(match).ToList();
        }

        public List<Customer> FindAll()
        {
            return _dbContext.Customers.ToList();
        }

        public void Remove(Customer entity)
        {
            _dbContext.Customers.Remove(entity);
        }

        public void Update(Customer entity)
        {
            //Not required!
        }

        public Customer FindByID(int id)
        {
            var results = _dbContext.Customers.Where(d => d.ID == id);

            return results.FirstOrDefault();
        }

        public void AddRange(List<Customer> customers)
        {
            _dbContext.Customers.AddRange(customers);
        }

    }
}