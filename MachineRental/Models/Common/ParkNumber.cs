using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MachineRental.Models.Common
{
    public class ParkNumber
    {
        public byte Id { get; set; }

        [Display(Name = "Numéro de parc")]
        [RegularExpression(@"^[0-9]", ErrorMessage = "Le numéro de parc peut comporter uniquement des chiffres")]
        //[MaxLength(6, ErrorMessage = "Merci de renseigner un numéro de parc compris entre 3 et 8 caractères"), 
        //MinLength(2, ErrorMessage = "Merci de renseigner un numéro de parc compris entre 3 et 8 caractères")]
        public string Number { get; set; }
    }
}