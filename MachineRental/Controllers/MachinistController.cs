using MachineRental.Models;
using MachineRental.Models.Common;
using MachineRental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;
using MachineRental.Common;

namespace MachineRental.Controllers
{
    public class MachinistController : Controller
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

        // GET: Machinists
        public ActionResult Index()
        {
            var viewmodel = _context.Machinist.Include(c => c.Address).ToList();
            return View("Machinists", viewmodel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new MachinistViewModel
            {
                Machinist = new Machinist(),
                Address = new Address()
            };

            return View("MachinistsForm", viewModel);
        }

        // GET: Machinists/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var machinist = _context.Machinist.Include(m => m.Address)
                                            .SingleOrDefault(c => c.Id == id);

            if (machinist == null)
                return HttpNotFound();

            var viewModel = new MachinistViewModel
            {
                Machinist = machinist,
                Address = machinist.Address
            };

            return View("MachinistsForm", viewModel);
        }

        // POST: Machinists/Create or Machinists/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent Cross Site Request Forgery (CSRF) Attacks by generating and comparing Token automatically
        public ActionResult Save(
            [Bind(Include = "Id, FirstName, LastName, Email, Phone, AddressId")] Machinist machinist,
            [Bind(Include = "Id, Address1, Address2, City, Country, ZipCode")] Address address)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MachinistViewModel
                {
                    Machinist = machinist,
                    Address = address
                };
                return View("MachinistsForm", viewModel);
            }

            // if new machinist
            if (machinist.Id == 0)
            {
                _context.Address.Add(address);
                _context.SaveChanges();
                machinist.AddressId = address.Id;
                _context.Machinist.Add(machinist);
                TempData[Constant.Success] = Constant.New;
            }
            // if modify existing machinist
            else
            {
                var MachinistDB = _context.Machinist.Single(c => c.Id == machinist.Id);
                var addressDB = _context.Address.Single(a => a.Id == address.Id);

                addressDB.Address1 = address.Address1;
                addressDB.Address2 = address.Address2;
                addressDB.ZipCode = address.ZipCode;
                addressDB.City = address.City;
                addressDB.Country = address.Country;

                MachinistDB.FirstName = machinist.FirstName;
                MachinistDB.LastName = machinist.LastName;
                MachinistDB.Email = machinist.Email;
                MachinistDB.Phone = machinist.Phone;
                machinist.IsAvailable = true;

                TempData[Constant.Success] = Constant.Update;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Machinist");
        }

        // DELETE : Machinists/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var machinist = _context.Machinist.SingleOrDefault(c => c.Id == id);

            if (machinist == null)
                return HttpNotFound();

            _context.Machinist.Remove(machinist);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}