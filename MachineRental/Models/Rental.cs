using MachineRental.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MachineRental.Models
{
    public class Rental
    {
        [Display(Name = "Numéro de location", ShortName = "Location n°")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        [Display(Name = "Début du chantier")]
        public DateTime TimeStartRentalDate { get; set; } = DateTime.Now;

        [Display(Name = "Fin de du chantier")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        public DateTime? TimeEndRentalDate { get; set; }

        [Required]
        [Display(Name = "Adresse du chantier", ShortName = "Chantier")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [StringLength(255)]
        [Display(Name = "Responsable de chantier", ShortName = "Responsable")]
        public string SiteManager { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
                        ErrorMessage = "Format email non valide")]
        [Display(Name = "Email du Responsable")]
        [StringLength(255)]
        public string SiteManagerEmail { get; set; }

        [RegularExpression(@"^[0-9\s+-]*$", ErrorMessage = "Format téléphone non valide")]
        [MaxLength(16), MinLength(8)]
        [Display(Name = "Téléphone du Responsable")]
        public string ManagerPhoneNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "Sous-traitant")]
        public string Subcontractor { get; set; }

        [Display(Name = "Prix Client")]
        public decimal? ClientPrice { get; set; }

        [Display(Name = "Prix Tranfert Client")]
        public decimal? ClientTransferPrice { get; set; }

        [Display(Name = "Prix sous-traitant", ShortName = "Prix s/traitant")]
        public decimal? SubcontractorPrice { get; set; }

        [Display(Name = "Prix Transfert s/t", ShortName = "Prix Transfert sous-traitant")]
        public decimal? SubcontractorTransferPrice { get; set; }

        [Display(Name = "Machine")]
        public int MachineId { get; set; }
        public Machine Machine { get; set; }

        [StringLength(255)]
        [Display(Name = "Equipement")]
        public string Equipment { get; set; }

        [Display(Name = "Conducteur")]
        public int MachinistId { get; set; }
        public Machinist Machinist { get; set; }

        [Required]
        [Display(Name = "Numéro de commande")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Statut de la location", ShortName = "Statut")]
        public string RentalStatus { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}