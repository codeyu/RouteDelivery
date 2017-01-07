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
    public class DeliveryController : Controller
    {
        private IUnitOfWork _uof;

        public DeliveryController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public ActionResult Index()
        {
            var Deliverys = _uof.Deliveries.FindAll();

            return View(Deliverys);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var delivery = new Delivery();

            return View(delivery);
        }

        [HttpPost]
        public ActionResult Create(Delivery newCreateDelivery)
        {
            if (ModelState.IsValid)
            {
                _uof.Deliveries.Add(newCreateDelivery);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newCreateDelivery);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var DeliveryEdit = _uof.Deliveries.FindByID(Id);

            return View(DeliveryEdit);
        }

        [HttpPost]
        public ActionResult Edit(Delivery EditedDelivery)
        {
            if (ModelState.IsValid)
            {
                _uof.Deliveries.Update(EditedDelivery);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(EditedDelivery);
        }

        public ActionResult Delete(int Id)
        {
            var DeliveryDel = _uof.Deliveries.FindByID(Id);

            if (DeliveryDel != null)
            {
                _uof.Deliveries.Remove(DeliveryDel);
                _uof.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}