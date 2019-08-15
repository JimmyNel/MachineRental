using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MachineRental.Models.Common
{
    public class SignatureImg
    {
        public int Id { get; set; }

        [Display(Name = "Nom du fichier")]
        public string Name { get; set; }

        public byte[] Data { get; set; }
    }
}