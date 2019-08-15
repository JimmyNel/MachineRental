using MachineRental.Common;
using MachineRental.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MachineRental.ViewModels;
using MachineRental.Models.Common;
using System.Net;
using Microsoft.AspNet.Identity;

namespace MachineRental.Controllers
{
    public class AttachmentController : Controller
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


        // GET: Attachments
        public async Task<ActionResult> Index()
        {
            var viewModel = new List<Attachment>();

            //var userConn = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindByEmailAsync(User.Identity.Name);

            if (User.IsInRole(Constant.Machinist))
            {
                var temp1 = await _context.Machinist.Where(m => m.Email == User.Identity.Name).Select(m => m.Id).FirstOrDefaultAsync();

                viewModel = _context.Attachment
                                .Include(s => s.SignatureImg)
                                .Include(r => r.Rental)
                                .Include(m => m.Rental.Machinist)
                                .Include(m => m.Rental.Machine)
                                .Include(a => a.Rental.Address)
                                .Include(c => c.Rental.Order.Customer)
                                .Where(m => m.Rental.MachinistId == temp1)
                                .ToList();

                return View("Attachments", viewModel);
            }


            viewModel = _context.Attachment
                            .Include(s => s.SignatureImg)
                            .Include(r => r.Rental)
                            .Include(m => m.Rental.Machinist)
                            .Include(m => m.Rental.Machine)
                            .Include(a => a.Rental.Address)
                            .Include(c => c.Rental.Order.Customer)
                            .ToList();

            return View("Attachments", viewModel);
        }

        // GET: Attachment/Create
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new AttachmentViewModel
            {
                Attachment = new Models.Attachment(),
                SignatureImg = new SignatureImg(),
                Customers = _context.Customer.ToList(),
                Rental = _context.Rental.Single(r => r.Id == 1)
            };

            return View("AttachmentsForm", viewModel);
        }

        // GET: Attachment/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var attachment = _context.Attachment
                                .Include(s => s.SignatureImg)
                                .Include(r => r.Rental)
                                .Include(m => m.Rental.Machinist)
                                .Include(m => m.Rental.Machine)
                                .Include(a => a.Rental.Address)
                                .Include(c => c.Rental.Order.Customer)
                                .SingleOrDefault(m => m.Id == id);

            if (attachment == null)
                return HttpNotFound();

            attachment.IsApproved = attachment.IsSigned = true;

            var viewModel = new AttachmentViewModel
            {
                Attachment = attachment,
                SignatureImg = attachment.SignatureImg,
                Rental = attachment.Rental,
                Customers = _context.Customer.ToList(),
                Hours = TimeSpan.FromTicks(attachment.WorkingHoursTicks).Hours,
                Minutes = TimeSpan.FromTicks(attachment.WorkingHoursTicks).Minutes
            };

            return View("AttachmentsForm", viewModel);
        }

        //[Authorize(Roles = Constant.Machinist)]
        [HttpGet]
        public ActionResult Generate(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var rental = _context.Rental
                .Include(a => a.Address)
                .Include(c => c.Order.Customer)
                .Include(m => m.Machinist)
                .Include(m => m.Machine)
                .SingleOrDefault(r => r.Id == id);

            if (rental == null)
                return HttpNotFound();

            var viewModel = new AttachmentViewModel
            {
                Attachment = new Attachment(),
                SignatureImg = new SignatureImg(),
                Rental = rental,
                Customers = _context.Customer.ToList()
            };

            return View("AttachmentsForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // Prevent Cross Site Request Forgery (CSRF) Attacks by generating and comparing Token automatically
        public async Task<ActionResult> Save(
        [Bind(Include = "Id, RentalStatus")] Rental rental,
        [Bind(Include = "Id, Data")] SignatureImg SignatureImg,
        [Bind(Include = "Id, AttachmentDate, Remarks, IsApproved, IsSigned")] Attachment attachment,
        string dataUrl, int hours, int minutes)
        {
            if (!ModelState.IsValid)
            {
                // if new attachment
                if (attachment.Id == 0)
                {
                    var viewModel = new AttachmentViewModel
                    {
                        Attachment = attachment,
                        SignatureImg = SignatureImg,
                        Rental = _context.Rental.Include(c => c.Order.Customer)
                                                .Include(c => c.Address)
                                                .Include(c => c.Machinist)
                                                .Include(m => m.Machine)
                                                .Single(r => r.Id == rental.Id),
                        dataUrl = dataUrl,
                        Customers = _context.Customer.ToList(),
                        Hours = hours,
                        Minutes = minutes
                    };
                    return View("AttachmentsForm", viewModel);
                }
                // if update attachment
                else if (attachment.Id != 0)
                {
                    var attachmentDb = _context.Attachment
                                        .Include(s => s.SignatureImg)
                                        .Include(r => r.Rental)
                                        .Include(m => m.Rental.Machinist)
                                        .Include(m => m.Rental.Machine)
                                        .Include(a => a.Rental.Address)
                                        .Include(c => c.Rental.Order.Customer)
                                        .SingleOrDefault(m => m.Id == attachment.Id);

                    var viewModel = new AttachmentViewModel
                    {
                        Attachment = attachmentDb,
                        SignatureImg = attachmentDb.SignatureImg,
                        Rental = attachmentDb.Rental,
                        Customers = _context.Customer.ToList(),
                        Hours = TimeSpan.FromTicks(attachmentDb.WorkingHoursTicks).Hours,
                        Minutes = TimeSpan.FromTicks(attachmentDb.WorkingHoursTicks).Minutes
                    };
                    return View("AttachmentsForm", viewModel);
                }
            }

            // if New attachment
            if (attachment.Id == 0)
            {
                byte[] bytes = Convert.FromBase64String(dataUrl.Split(',')[1]);
                SignatureImg.Data = bytes;
                _context.SignatureImg.Add(SignatureImg);
                _context.SaveChanges();

                attachment.SignatureImgId = SignatureImg.Id;
                attachment.RentalId = rental.Id;

                var workingHours = new TimeSpan(hours, minutes, 0);
                attachment.WorkingHoursTicks = workingHours.Ticks;

                _context.Attachment.Add(attachment);
                _context.SaveChanges();


                var attachmentFinal = _context.Attachment
                                .Include(c => c.Rental.Order.Customer)
                                .Include(m => m.Rental.Machinist)
                                .Include(m => m.Rental.Address)
                                .SingleOrDefault(m => m.Id == attachment.Id);

                SignatureImg.Name = attachmentFinal.Rental.Order.Customer.Name +
                                "-" + attachmentFinal.Rental.Machinist.LastName +
                                "-" + attachment.AttachmentDate.ToString("ddMMyyyy");

                TempData[Constant.Success] = Constant.New;

                // manage email sending to the customer
                // sender configuration + smtp in webconfig file
                try
                {
                    //var receiverEmail = new MailAddress("jimmy.neller@gmail.com", attachmentFinal.Rental.Order.Customer.Name);
                    var sub = "MOREL - Attachement journalier n°" + attachmentFinal.Id +
                        " du " + attachmentFinal.AttachmentDate.ToString("dd/MM/yyyy") +
                        " - Chantier : " + attachmentFinal.Rental.Address.City;
                    var body = "Contenu du mail";
                    var message = new IdentityMessage
                    {
                        Destination = "jimmy.neller@gmail.com",
                        Body = body,
                        Subject = sub
                    };

                    // use emailService class
                    var emailService = new EmailService();
                    await emailService.SendAsync(message);

                }
                catch (Exception e)
                {
                    return View("Error", e);
                }
            }

            // if Update attachment
            else
            {
                var attachmentDB = _context.Attachment.Single(c => c.Id == attachment.Id);

                attachmentDB.IsApproved = attachmentDB.IsSigned = true;

                attachmentDB.Remarks = attachment.Remarks;
                attachmentDB.AttachmentDate = attachment.AttachmentDate;
                attachmentDB.WorkingHoursTicks = new TimeSpan(hours, minutes, 0).Ticks;

                TempData[Constant.Success] = Constant.Update;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Attachment");
        }

        // DELETE : Attachments/Delete/{id}
        //[Authorize(Roles = Constant.Admin +", "+ Constant.RentalManager)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var attachment = _context.Attachment.SingleOrDefault(c => c.Id == id);

            if (attachment == null)
                return HttpNotFound();

            _context.Attachment.Remove(attachment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}