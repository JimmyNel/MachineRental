using MachineRental.Models;
using MachineRental.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineRental.ViewModels
{
    public class AttachmentViewModel
    {
        public Attachment Attachment { get; set; }

        public Rental Rental { get; set; }

        public SignatureImg SignatureImg { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public string dataUrl { get; set; }
    }
}