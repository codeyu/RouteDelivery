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
    public class DriverController : Controller
    {
        private IUnitOfWork _uof;

        public DriverController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public ActionResult Index()
        {
            var drivers = _uof.Drivers.FindAll();

            return View(drivers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var driver = new Driver();

            return View(driver);
        }

        [HttpPost]
        public ActionResult Create(Driver newCreateDriver)
        {
            if (ModelState.IsValid)
            {
                _uof.Drivers.Add(newCreateDriver);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newCreateDriver);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var driverEdit = _uof.Drivers.FindByID(Id);

            return View(driverEdit);
        }

        [HttpPost]
        public ActionResult Edit(Driver EditedDriver)
        {
            if (ModelState.IsValid)
            {
                _uof.Drivers.Update(EditedDriver);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(EditedDriver);
        }

        public ActionResult Delete(int Id)
        {
            var driverDel = _uof.Drivers.FindByID(Id);

            if (driverDel != null)
            {
                _uof.Drivers.Remove(driverDel);
                _uof.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}