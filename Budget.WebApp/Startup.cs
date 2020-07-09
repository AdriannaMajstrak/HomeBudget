using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Budget.WebApp.Startup))]
namespace Budget.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
