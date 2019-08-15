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
    public class MachineController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // DBContext is a disposable object => need to be disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Machines
        [HttpGet]
        public ActionResult Index()
        {
            var viewmodel = _context.Machine
                                .Include(m => m.MachineType)
                                .Include(p => p.ParkNumber)
                                .Include(m => m.AddressPosition)
                                .ToList();


            return View("Machines", viewmodel);
        }

        // GET: Machines/Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new MachineViewModel
            {
                ParkNumbers = _context.ParkNumber.ToList(),
                MachineTypes = _context.MachineType.ToList(),
                Machine = new Machine(),
                Address = new Address()
            };

            return View("MachinesForm", viewModel);
        }

        // GET: Machines/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var machine = _context.Machine.Include(m => m.AddressPosition)
                                            .SingleOrDefault(m => m.Id == id);

            if (machine == null)
                return HttpNotFound();

            var viewModel = new MachineViewModel
            {
                ParkNumbers = _context.ParkNumber.ToList(),
                MachineTypes = _context.MachineType.ToList(),
                Machine = machine,
                Address = machine.AddressPosition
            };

            return View("MachinesForm", viewModel);
        }

        // POST: Machines/Create or Machines/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent Cross Site Request Forgery (CSRF) Attacks by generating and comparing Token automatically
        public ActionResult Save(
            [Bind(Include = "Id, Number, MachineTypeId, ParkNumberId, AddressId")] Machine machine,
            [Bind(Include = "Id, Address1, Address2, City, Country, ZipCode")] Address address)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MachineViewModel
                {
                    ParkNumbers = _context.ParkNumber.ToList(),
                    MachineTypes = _context.MachineType.ToList(),
                    Machine = machine,
                    Address = address
                };
                return View("MachinesForm", viewModel);
            }

            // if new machine
            if (machine.Id == 0)
            {
                _context.Address.Add(address);
                _context.SaveChanges();
                machine.AddressId = address.Id;
                machine.IsRented = false;
                _context.Machine.Add(machine);
                TempData[Constant.Success] = Constant.New;
            }
            // if modify existing machine
            else
            {
                var machineDB = _context.Machine.Single(m => m.Id == machine.Id);
                var addressDB = _context.Address.Single(a => a.Id == address.Id);

                addressDB.Address1 = address.Address1;
                addressDB.Address2 = address.Address2;
                addressDB.ZipCode = address.ZipCode;
                addressDB.City = address.City;
                addressDB.Country = address.Country;

                machineDB.Number = machine.Number;
                machineDB.MachineTypeId = machine.MachineTypeId;
                machineDB.ParkNumberId = machine.MachineTypeId;

                TempData[Constant.Success] = Constant.Update;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Machine");
        }

        // DELETE : Machines/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var machine = _context.Machine.SingleOrDefault(m => m.Id == id);

            if (machine == null)
                return HttpNotFound();

            _context.Machine.Remove(machine);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}