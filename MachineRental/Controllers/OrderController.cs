using MachineRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using MachineRental.ViewModels;
using System.Net;
using MachineRental.Common;

namespace MachineRental.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Orders
        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = _context.Order
                .Include(c => c.Customer)
                .Include(c => c.Rentals)
                .ToList();

            return View("Orders", viewModel);
        }

        // GET: Orders/Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new OrderViewModel
            {
                Customers = _context.Customer.ToList(),
                Order = new Order()
            };

            return View("OrdersForm", viewModel);
        }

        // GET: Orders/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var order = _context.Order
                                .Include(c => c.Customer)
                                .Include(c => c.Rentals)
                                .SingleOrDefault(c => c.Id == id);

            if (order == null)
                return HttpNotFound();

            var viewModel = new OrderViewModel
            {
                Customers = _context.Customer.ToList(),
                Order = order
            };

            return View("OrdersForm", viewModel);
        }

        // POST: Orders/Create or Orders/Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Save(
            [Bind(Include = "Id, CustomerId, OrderDate")] Order order)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new OrderViewModel
                {
                    Order = order,
                    Customers = _context.Customer.ToList()
                };
                return View("OrdersForm", viewModel);
            }

            // if new order
            if (order.Id == 0)
            {
                order.Rentals = new List<Rental>();
                _context.Order.Add(order);
                _context.SaveChanges();

                TempData[Constant.Success] = Constant.New;
            }
            // if modify existing order
            else
            {
                var orderDB = _context.Order.SingleOrDefault(o => o.Id == order.Id);

                orderDB.OrderDate = order.OrderDate;
                orderDB.CustomerId = order.CustomerId;
                _context.SaveChanges();

                TempData[Constant.Success] = Constant.Update;
            }

            return RedirectToAction("Index", "Order");
        }

        // DELETE : Orders/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var order = _context.Order.SingleOrDefault(m => m.Id == id);

            if (order == null)
                return HttpNotFound();

            _context.Order.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}