using MachineRental.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MachineRental.Models
{
    public class Machine
    {
        public int Id { get; set; }

        [Required]
        [StringLength(24)]
        [Display(Name = "Numéro véhicule")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Type de véhicule", ShortName = "Type")]
        public byte MachineTypeId { get; set; }
        public MachineType MachineType { get; set; }

        [Required]
        [Display(Name = "Numéro de parc", ShortName = "Parc")]
        public byte ParkNumberId { get; set; }
        public ParkNumber ParkNumber { get; set; }

        [Display(Name = "Disponibilité")]
        public bool IsRented { get; set; }

        [Display(Name = "Emplacement actuel")]
        public int AddressId { get; set; }
        public Address AddressPosition { get; set; }

    }
}