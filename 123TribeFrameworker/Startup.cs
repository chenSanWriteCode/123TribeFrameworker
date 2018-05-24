using System.Web.Mvc;
using _123TribeFrameworker.Services;
using _123TribeFrameworker.Services.Layer;
using Microsoft.Owin;
using Owin;
using Unity;
using Unity.AspNet.Mvc;

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
