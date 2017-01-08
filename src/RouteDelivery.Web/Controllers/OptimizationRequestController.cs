
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using RouteDelivery.Data;
using RouteDelivery.Models;
using RouteDelivery.OptimizationEngine;
namespace RouteDelivery.Web.Controllers
{
    public class OptimizationRequestController : Controller
    {
        private IUnitOfWork _uof;
        private IOptimizationEngine _optiEngine;

        public OptimizationRequestController(IUnitOfWork uof)
        {
            _uof = uof;
            _optiEngine = new OptimizationEngine.OptimizationEngine(_uof);
        }

        public ActionResult Index()
        {
            var optimizationRequests = _uof.OptimizationRequests.FindAll();

            return View(optimizationRequests);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var optimizationRequest= new OptimizationRequest();

            return View(optimizationRequest);
        }

        [HttpPost]
        public ActionResult Create(OptimizationRequest newOptimizationRequest)
        {
            if (ModelState.IsValid)
            {
                _uof.OptimizationRequests.Add(newOptimizationRequest);
                _uof.SaveChanges();

                var id = BackgroundJob.Enqueue(() => _optiEngine.OptimizeDeliveries(newOptimizationRequest.ID));

                return RedirectToAction("Index");
            }

            return View(newOptimizationRequest);
        }


        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var optimizationRequestEdit = _uof.OptimizationRequests.FindByID(Id);

            return View(optimizationRequestEdit);
        }

        [HttpPost]
        public ActionResult Edit(OptimizationRequest OptimizationRequestEdit)
        {
            if (ModelState.IsValid)
            {
                _uof.OptimizationRequests.Update(OptimizationRequestEdit);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(OptimizationRequestEdit);
        }

        public ActionResult Delete(int Id)
        {
            var OptimizationRequestDel = _uof.OptimizationRequests.FindByID(Id);

            if (OptimizationRequestDel != null)
            {
                _uof.OptimizationRequests.Remove(OptimizationRequestDel);
                _uof.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}