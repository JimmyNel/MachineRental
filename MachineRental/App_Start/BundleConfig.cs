using System.Web;
using System.Web.Optimization;

namespace MachineRental
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-1.12.4.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js",
                        "~/Scripts/DataTables/dataTables.bootstrap.min.js",
                        "~/Scripts/propeller.js",
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/Site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/form-fiche-salarie").Include(
                        "~/Scripts/form-fiche-salarie.js"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/typography.css",
                        "~/Content/google-icons.css",
                        "~/Content/google-material-icons.css",
                        "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                        "~/Content/pmd-datatable.css",
                        "~/Content/toastr.css",
                        "~/Content/bootstrap.css",
                        "~/Content/propeller.css",
                        "~/Content/site.css",
                        "~/Content/jquery-ui.css"));
        }
    }
}
