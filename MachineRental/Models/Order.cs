using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MachineRental.Models
{
    public class Order
    {
        [Display(Name = "Numéro de commande", ShortName = "Numero")]
        public int Id { get; set; }

        [Display(Name = "Date de commande")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Client")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Display(Name = "Statut")]
        public string Status { get; set; }

        [Display(Name = "Nombre de locations")]
        public ICollection<Rental> Rentals { get; set; }
    }
}