using MachineRental.Models;
using MachineRental.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineRental.ViewModels
{
    public class MachinistViewModel
    {
        public Machinist Machinist { get; set; }

        public Address Address { get; set; }
    }
}