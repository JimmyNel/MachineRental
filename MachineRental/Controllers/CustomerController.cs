using MachineRental.Common;
using MachineRental.Models;
using MachineRental.Models.Common;
using MachineRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MachineRental.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = _context.Customer.Include(c => c.Address).ToList();

            return View("Customers", viewModel);
        }

        // GET: Customers/Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CustomerViewModel
            {
                Customer = new Customer(),
                Address = new Address()
            };

            return View("CustomersForm", viewModel);
        }

        // GET: Customers/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var machine = _context.Customer.Include(m => m.Address)
                                            .SingleOrDefault(c => c.Id == id);

            if (machine == null)
                return HttpNotFound();

            var viewModel = new CustomerViewModel
            {
                Customer = machine,
                Address = machine.Address
            };

            return View("CustomersForm", viewModel);
        }

        // POST: Customers/Create or Customers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(
            [Bind(Include = "Id, Name, Email, Phone, Contact, AddressId")] Customer customer,
            [Bind(Include = "Id, Address1, Address2, City, Country, ZipCode")] Address address)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerViewModel
                {
                    Customer = customer,
                    Address = address
                };
                return View("CustomersForm", viewModel);
            }

            // if new machine
            if (customer.Id == 0)
            {
                _context.Address.Add(address);
                _context.SaveChanges();
                customer.AddressId = address.Id;
                _context.Customer.Add(customer);
                TempData[Constant.Success] = Constant.New;
            }
            // if modify existing machine
            else
            {
                var customerDB = _context.Customer.Single(c => c.Id == customer.Id);
                var addressDB = _context.Address.Single(a => a.Id == address.Id);

                addressDB.Address1 = address.Address1;
                addressDB.Address2 = address.Address2;
                addressDB.ZipCode = address.ZipCode;
                addressDB.City = address.City;
                addressDB.Country = address.Country;

                customerDB.Name = customer.Name;
                customerDB.Contact = customer.Contact;
                customerDB.Email = customer.Email;
                customerDB.Phone = customer.Phone;

                TempData[Constant.Success] = Constant.Update;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        // DELETE : Customers/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var customer = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            _context.Customer.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}