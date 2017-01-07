using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RouteDelivery.Data;
using RouteDelivery.Models;
namespace RouteDelivery.Web.Controllers
{
    public class DriverScheduleController : Controller
    {
        private IUnitOfWork _uof;

        public DriverScheduleController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public ActionResult Index(int Id)
        {
            var DriverSchedules = _uof.DeliverySchedule.Find(d => d.OptimizationRequestID == Id);

            if (DriverSchedules != null)
            {
                return View(DriverSchedules);
            }
            else
            {
                return RedirectToAction("Index", "OptimizationRequestController");
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            var DriverSchedule = new DeliverySchedule();

            return View(DriverSchedule);
        }

        [HttpPost]
        public ActionResult Create(DeliverySchedule newDriverSchedules)
        {
            if (ModelState.IsValid)
            {
                _uof.DeliverySchedule.Add(newDriverSchedules);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newDriverSchedules);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var DriverScheduleEdit = _uof.DeliverySchedule.FindByID(Id);

            return View(DriverScheduleEdit);
        }

        [HttpPost]
        public ActionResult Edit(DeliverySchedule DriverScheduleEdit)
        {
            if (ModelState.IsValid)
            {
                _uof.DeliverySchedule.Update(DriverScheduleEdit);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(DriverScheduleEdit);
        }

        public ActionResult Delete(int Id)
        {
            var DriverScheduleDel = _uof.DeliverySchedule.FindByID(Id);

            if (DriverScheduleDel != null)
            {
                _uof.DeliverySchedule.Remove(DriverScheduleDel);
                _uof.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}