using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MachineRental.Models.Common
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Adresse 1")]
        [StringLength(255)]
        public string Address1 { get; set; }

        [Display(Name = "Adresse 2")]
        [StringLength(255)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "Code Postal")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Ville", ShortName = "Chantier")]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Pays")]
        [StringLength(255)]
        public string Country { get; set; }
    }
}