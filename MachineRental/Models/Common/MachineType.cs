using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MachineRental.Models.Common
{
    public class MachineType
    {
        public byte Id { get; set; }

        [Display(Name = "Type de machine")]
        [StringLength(255)]
        public string Name { get; set; }
    }
}