using RouteDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RouteDelivery.Data.Implementations
{
    public class OptimizationRequestRepository: IRepository<OptimizationRequest>
    {
        private RouteDeliveryDbContext _dbContext;

        public OptimizationRequestRepository(RouteDeliveryDbContext dbContext)
        {
            _dbContext = dbContext;
            if (_dbContext.OptimizationRequests.Count() == 0)
            {
                AddRange(GetMockOptimizationRequestData());
                _dbContext.SaveChanges();
            }
        }

        private List<OptimizationRequest> GetMockOptimizationRequestData()
        {
            return new List<OptimizationRequest>() {
                new OptimizationRequest() { ID = 1, ScheduleDate = new DateTime(2016, 01, 01, 10, 0, 0), Status = RequestStatus.Complete}
            };
        }

        public void Add(OptimizationRequest newEntity)
        {
            //newEntity.ID = (_OptimizationRequests.Max(r => r.ID) + 1);
            _dbContext.OptimizationRequests.Add(newEntity);
        }

        public List<OptimizationRequest> Find(Func<OptimizationRequest, bool> match)
        {
            return _dbContext.OptimizationRequests.Where(match).ToList();
        }

        public List<OptimizationRequest> FindAll()
        {
            return _dbContext.OptimizationRequests.ToList();
        }

        public void Remove(OptimizationRequest entity)
        {
            _dbContext.OptimizationRequests.Remove(entity);
        }

        public void Update(OptimizationRequest entity)
        {
        }

        public OptimizationRequest FindByID(int id)
        {
            var results = _dbContext.OptimizationRequests.Where(d => d.ID == id);

            return results.FirstOrDefault();
        }

        public void AddRange(List<OptimizationRequest> OptimizationRequests)
        {
            _dbContext.OptimizationRequests.AddRange(OptimizationRequests);
        }

    }
}
