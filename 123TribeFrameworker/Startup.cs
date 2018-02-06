using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_123TribeFrameworker.Startup))]
namespace _123TribeFrameworker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
