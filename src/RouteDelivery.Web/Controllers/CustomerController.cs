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
    public class CustomerController : Controller
    {
        private IUnitOfWork _uof;

        public CustomerController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public ActionResult Index()
        {
            var Customers = _uof.Customers.FindAll();

            return View(Customers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var customer = new Customer();

            return View(customer);
        }

        [HttpPost]
        public ActionResult Create(Customer newCreateCustomer)
        {
            if (ModelState.IsValid)
            {
                _uof.Customers.Add(newCreateCustomer);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(newCreateCustomer);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var CustomerEdit = _uof.Customers.FindByID(Id);

            return View(CustomerEdit);
        }

        [HttpPost]
        public ActionResult Edit(Customer EditedCustomer)
        {
            if (ModelState.IsValid)
            {
                _uof.Customers.Update(EditedCustomer);
                _uof.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(EditedCustomer);
        }

        public ActionResult Delete(int Id)
        {
            var CustomerDel = _uof.Customers.FindByID(Id);

            if (CustomerDel != null)
            {
                _uof.Customers.Remove(CustomerDel);
                _uof.SaveChanges();
            }

            return RedirectToAction("Index");
        }



    }
}