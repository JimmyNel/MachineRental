using MachineRental.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace MachineRental.Models
{
    public class Machinist
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nom du conducteur", ShortName ="Conducteur")]
        public string LastName { get; set; }

        [Display(Name = "Adresse")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Format email non valide")]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9\s+-]*$")]
        [MaxLength(16), MinLength(8)]
        [Display(Name = "Téléphone")]
        public string Phone { get; set; }

        [Display(Name = "Disponible")]
        public bool IsAvailable { get; set; }

    }
}