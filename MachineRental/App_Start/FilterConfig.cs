using System.Web;
using System.Web.Mvc;

namespace MachineRental
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Global filter - Authentication required for any page of the app
            filters.Add(new AuthorizeAttribute());
        }
    }
}
