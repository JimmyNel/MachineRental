using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineRental.Common
{
    public class Constant
    {

        public static readonly IEnumerable<string> rentalStatus = new List<string>()
        {
            Ordered,
            InProgress,
            Closed
        };

        // define users roles
        public static readonly IEnumerable<string> Roles = new List<string>()
        {
            RentalManager,
            Machinist,
            Admin
        };

        public const string InUse = "En service";
        public const string Available = "Disponible";
        public const string Signed = "Signé";
        public const string NotSigned = "Non signé";

        public const string Ordered = "Commandée";
        public const string InProgress = "En cours";
        public const string Closed = "Terminée";

        public const string RentalManager = "RentalManager";
        public const string Machinist = "Machinist";
        public const string Admin = "Admin";

        public const string NewMachineSaveSuccess = "Nouvelle machine enregistrée avec succès";
        public const string MachineUpdateSuccess = "Machine mise à jour avec succès";
        public const string NewMachinistSaveSuccess = "Nouveau conducteur enregistré avec succès";
        public const string MachinistUpdateSuccess = "Conducteur mis à jour avec succès";
        public const string NewCustomerSaveSuccess = "Nouveau client enregistré avec succès";
        public const string CustomerUpdateSuccess = "Client mis à jour avec succès";
        public const string NewAttachmentSaveSuccess = "Nouvel attachement journalier enregistré avec succès";
        public const string AttachmentUpdateSuccess = "Attachement journalier mis à jour avec succès";
        public const string NewOrderSaveSuccess = "Nouvelle commande enregistrée avec succès";
        public const string OrderUpdateSuccess = "Commande mise à jour avec succès";
        public const string NewUserSaveSuccess = "Nouvel utilisateur enregistré avec succès";
        public const string UserUpdateSuccess = "Utilisateur mis à jour avec succès";
        public const string NewRentalSaveSuccess = "Nouvelle location enregistrée avec succès";
        public const string RentalUpdateSuccess = "Location mise à jour avec succès";
        public const string NewPasswordSuccess = "Mot de passe enregistré avec succès";
        public const string PasswordUpdateSuccess = "Mot de passe mis à jour avec succès";

        public const string New = "new";
        public const string Update = "update";
        public const string Success = "success";

        public const string DefaultPassword = "*****";

        public const string ErrorSetPassword = "Une erreur s'est produite. Merci de recommencer.";

        public const string EmailSender = "*****";
        public const string Morel = "Morel";
    }
}