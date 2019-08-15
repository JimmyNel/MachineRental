using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MachineRental.Startup))]
namespace MachineRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
