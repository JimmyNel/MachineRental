using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MachineRental.ViewModels
{
    public class UserInRoleViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Email")]
        public string Username { get; set; }

        [Display(Name = "Actif ?")]
        public bool IsEnabled { get; set; }

        [Display(Name = "Email confirmé ?")]
        public bool EmailConfirmed { get; set; }

        public string RoleId { get; set; }
        public string Role { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}