using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using MachineRental.Models;
using MachineRental.Common;
using System.Net;
using MachineRental.ViewModels;
using MachineRental.Models.Common;
using System.Threading.Tasks;

namespace MachineRental.Controllers
{
    public class RentalController : Controller
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

        // GET: Rentals
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Rental> viewModel = null;

            if (User.IsInRole(Constant.Machinist))
            {
                var temp1 = await _context.Machinist.Where(m => m.Email == User.Identity.Name).Select(m => m.Id).FirstOrDefaultAsync();


                viewModel = _context.Rental
                    .Include(m => m.Machinist)
                    .Include(m => m.Machine)
                    .Include(o => o.Order)
                    .Include(a => a.Address)
                    .Include(at => at.Attachments)
                    .Include(c => c.Order.Customer)
                    .Where(m => m.MachinistId == temp1)
                    .ToList();

                return View("RentalsMachinist", viewModel);
            }


            viewModel = _context.Rental
                .Include(o => o.Order)
                .Include(a => a.Address)
                .Include(m => m.Machine)
                .Include(at => at.Attachments)
                .Include(c => c.Order.Customer)
                .ToList();

            return View("Rentals", viewModel);
        }

        // GET: Rentals
        [HttpGet]
        [Route("Rental/{id}")]
        public ActionResult RentalsByOrder(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = _context.Rental
                .Include(m => m.Machinist)
                .Include(m => m.Machine)
                .Include(o => o.Order)
                .Include(a => a.Address)
                .Include(at => at.Attachments)
                .Include(c => c.Order.Customer)
                .Where(m => m.OrderId == id)
                .ToList();

            return View("Rentals", viewModel);
        }

        // GET: Rentals/Create/{id}
        [HttpGet]
        public ActionResult Create(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var order = _context.Order
                            .Include(c => c.Customer)
                            .Include(c => c.Rentals)
                            .SingleOrDefault(o => o.Id == id);

            if (order == null)
                return HttpNotFound();

            var viewModel = new RentalFormViewModel
            {
                Machinists = _context.Machinist.ToList(),
                Address = new Address(),
                Machines = _context.Machine.ToList(),
                Rental = new Rental(),
                Order = order,
                RentalStatus = Constant.rentalStatus
            };

            return View("RentalsForm", viewModel);
        }

        // GET: Rentals/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var rental = _context.Rental
                            .Include(o => o.Order)
                            .Include(c => c.Order.Rentals)
                            .Include(a => a.Address)
                            .Include(m => m.Machine)
                            .Include(at => at.Attachments)
                            .Include(c => c.Order.Customer)
                            .SingleOrDefault(r => r.Id == id);

            if (rental == null)
                return HttpNotFound();

            var viewModel = new RentalFormViewModel
            {
                Rental = rental,
                Machinists = _context.Machinist.ToList(),
                Machines = _context.Machine.ToList(),
                Order = rental.Order,
                RentalStatus = Constant.rentalStatus,
                Address = rental.Address
            };

            return View("RentalsForm", viewModel);
        }

        //[Authorize]
        // POST: Rentals/Create or Rentals/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent Cross Site Request Forgery (CSRF) Attacks by generating and comparing Token automatically
        [Route("Rental/Save")]
        public ActionResult Save(
            [Bind(Include = "Id, TimeStartRentalDate, TimeEndRentalDate, SiteManager, " +
           "ManagerPhoneNumber, Subcontractor, ClientPrice, ClientTransferPrice, SubcontractorPrice, SubcontractorTransferPrice, " +
           "Equipment, RentalStatus, SiteManagerEmail, MachineId, MachinistId, AddressId")] Rental rental,
            [Bind(Include = "Id, Address1, Address2, City, Country, ZipCode")] Address address,
            [Bind(Include = "Id")] Order order)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new RentalFormViewModel
                {
                    Rental = rental,
                    Machinists = _context.Machinist.ToList(),
                    Machines = _context.Machine.ToList(),
                    Order = _context.Order
                            .Include(c => c.Customer)
                            .Include(c => c.Rentals)
                            .SingleOrDefault(o => o.Id == order.Id),
                    RentalStatus = Constant.rentalStatus,
                    Address = address
                };

                return View("RentalsForm", viewModel);
            }

            // if new rental
            if (rental.Id == 0)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Address.Add(address);
                    _context.SaveChanges();

                    rental.OrderId = order.Id;
                    rental.AddressId = address.Id;
                    _context.Rental.Add(rental);
                    _context.SaveChanges();

                    transaction.Commit();

                    TempData[Constant.Success] = Constant.New;
                }
            }

            // if modify existing rental
            else
            {
                var rentalDB = _context.Rental.Single(a => a.Id == rental.Id);
                var addressDB = _context.Address.Single(a => a.Id == address.Id);

                addressDB.Address1 = address.Address1;
                addressDB.Address2 = address.Address2;
                addressDB.ZipCode = address.ZipCode;
                addressDB.City = address.City;
                addressDB.Country = address.Country;

                rentalDB.TimeStartRentalDate = rental.TimeStartRentalDate;
                rentalDB.TimeEndRentalDate = rental.TimeEndRentalDate;
                rentalDB.SiteManager = rental.SiteManager;
                rentalDB.SiteManagerEmail = rental.SiteManagerEmail;
                rentalDB.ManagerPhoneNumber = rental.ManagerPhoneNumber;
                rentalDB.Subcontractor = rental.Subcontractor;
                rentalDB.SubcontractorPrice = rental.SubcontractorPrice;
                rentalDB.SubcontractorTransferPrice = rental.SubcontractorTransferPrice;
                rentalDB.ClientPrice = rental.ClientPrice;
                rentalDB.ClientTransferPrice = rental.ClientTransferPrice;
                rentalDB.Equipment = rental.Equipment;
                rentalDB.RentalStatus = rental.RentalStatus;
                rentalDB.MachineId = rental.MachineId;
                rentalDB.MachinistId = rental.MachinistId;

                _context.SaveChanges();
                TempData[Constant.Success] = Constant.Update;
            }

            return RedirectToAction("Index", "Rental");
        }

        // DELETE : Rentals/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var rental = _context.Rental.SingleOrDefault(m => m.Id == id);

            if (rental == null)
                return HttpNotFound();

            _context.Rental.Remove(rental);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}