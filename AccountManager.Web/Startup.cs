using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccountManager.Web.Startup))]
namespace AccountManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
