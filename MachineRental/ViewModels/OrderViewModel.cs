using MachineRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineRental.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<Customer> Customers { get; set; }
    }
}