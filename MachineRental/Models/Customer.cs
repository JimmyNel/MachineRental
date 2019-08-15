using MachineRental.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MachineRental.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nom du client")]
        public string Name { get; set; }

        [Display(Name = "Adresse du client", ShortName = "Adresse")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            ErrorMessage = "Format email non valide")]
        [StringLength(255)]
        public string Email { get; set; }

        [Display(Name = "Personne à contacter", ShortName = "Contact")]
        [StringLength(255)]
        public string Contact { get; set; }

        [Required]
        [Display(Name = "Téléphone")]
        [RegularExpression(@"^[0-9\s+-]*$")]
        [MaxLength(16), MinLength(8)]
        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}