using MachineRental.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MachineRental.Models
{
    public class Attachment
    {
        [Display(Name = "Numéro")]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        [Display(Name = "Date du bon")]
        public DateTime AttachmentDate { get; set; } = DateTime.Now;

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/mm/yyyy")]
        //[Display(Name = "Date de signature")]
        //public DateTime AttachmentSignatureDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Heures de travail")]
        public long WorkingHoursTicks { get; set; }

        [Display(Name = "Observations")]
        public string Remarks { get; set; }

        [NotMapped]
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Merci de cocher ce champs avant soumission du formulaire")]
        [Display(Name = "Valider l'exactitude des informations avant envoi")]
        public bool IsApproved { get; set; }

        [NotMapped]
        [Display(Name = "Ajouter Signature Client", ShortName = "Signature")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Le champ Signature Client est requis")]
        public bool IsSigned { get; set; }

        [Required]
        [Display(Name = "Signature du responsable de chantier")]
        public int SignatureImgId { get; set; }
        public SignatureImg SignatureImg { get; set; }

        public int RentalId { get; set; }
        public Rental Rental { get; set; }
    }
}