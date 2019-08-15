using MachineRental.Models;
using MachineRental.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineRental.ViewModels
{
    public class MachineViewModel
    {
        public Machine Machine { get; set; }

        public IEnumerable<ParkNumber> ParkNumbers { get; set; }

        public IEnumerable<MachineType> MachineTypes { get; set; }

        public Address Address { get; set; }
    }
}