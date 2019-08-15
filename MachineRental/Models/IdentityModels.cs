using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MachineRental.Models.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MachineRental.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser. Pour en savoir plus, consultez https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Utilisateur activé ?")]
        public bool IsEnabled { get; set; }

        [Required]
        [Display(Name = "Mot de passe initialisé ?")]
        public bool HasPasswordSet { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MachineRental", throwIfV1Schema: false)
        {
        }

        #region DbSet
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<Machinist> Machinist { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<MachineType> MachineType { get; set; }
        public DbSet<Rental> Rental { get; set; }
        public DbSet<SignatureImg> SignatureImg { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<ParkNumber> ParkNumber { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // turn off cascade delete Machine/ParkNumber
            modelBuilder.Entity<Machine>()
                .HasRequired(p => p.ParkNumber)
                .WithMany()
                .WillCascadeOnDelete(false);

            // turn off cascade delete Machine/MachineType
            modelBuilder.Entity<Machine>()
                .HasRequired(p => p.MachineType)
                .WithMany()
                .WillCascadeOnDelete(false);

            // turn off cascade delete Rental/Order
            modelBuilder.Entity<Rental>()
                .HasRequired(o => o.Order)
                .WithMany(r => r.Rentals)
                .WillCascadeOnDelete(false);

            // turn off cascade delete Rental/Machine
            modelBuilder.Entity<Rental>()
                .HasRequired(p => p.Machine)
                .WithMany()
                .WillCascadeOnDelete(false);

            // turn off cascade delete Rental/Machinist
            modelBuilder.Entity<Rental>()
                .HasRequired(p => p.Machinist)
                .WithMany()
                .WillCascadeOnDelete(false);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // override SaveChanges method to get more information in case of exception
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}