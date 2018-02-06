using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using _123TribeFrameworker.Controllers;
using _123TribeFrameworker.Models;
using _123TribeFrameworker.Services;
using _123TribeFrameworker.Services.Layer;
using Unity;
using Unity.AspNet.Mvc;

namespace _123TribeFrameworker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //IUnityContainer container = new UnityContainer();
            //container.RegisterType<IFirstLevelDir, FirstLevelDir>();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //MyControllerFactory factory = new MyControllerFactory(container);
            //ControllerBuilder.Current.SetControllerFactory(factory);
            ////注入
            //var container = this.BuildUnityContainer();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
                                                
       //IUnityContainer BuildUnityContainer()
       // {
       //     var container = new UnityContainer();
       //     container.RegisterType<IFirstLevelDir, FirstLevelDir>();
       //     return container;
       // }
    }
}
