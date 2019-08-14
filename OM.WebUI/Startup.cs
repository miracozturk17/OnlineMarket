using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OM.WebUI.Startup))]
namespace OM.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}