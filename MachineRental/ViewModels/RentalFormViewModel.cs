using MachineRental.Models;
using MachineRental.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineRental.ViewModels
{
    public class RentalFormViewModel
    {
        public Rental Rental { get; set; }

        public Order Order { get; set; }

        public IEnumerable<string> RentalStatus { get; set; }

        public IEnumerable<Machine> Machines { get; set; }

        public IEnumerable<Machinist> Machinists { get; set; }

        public Address Address { get; set; }
    }
}