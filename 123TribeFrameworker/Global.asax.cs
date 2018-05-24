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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

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
            //加载容器-注册依赖
            //BootStrapper.Initialise();
        }
    }
    //public class BootStrapper
    //{
    //    /// <summary>
    //    /// 获取容器-注册依赖关系
    //    /// </summary>
    //    /// <returns></returns>
    //    public static IUnityContainer Initialise()
    //    {
    //        var container = BuildUnityContainer();

    //        DependencyResolver.SetResolver(new UnityDependencyResolver(container));

    //        return container;
    //    }

    //    /// <summary>
    //    /// 加载容器
    //    /// </summary>
    //    /// <returns></returns>
    //    private static IUnityContainer BuildUnityContainer()
    //    {
    //        var container = new UnityContainer();

    //        RegisterTypes(container);

    //        return container;
    //    }

    //    /// <summary>
    //    /// 实施依赖注入
    //    /// </summary>
    //    /// <param name="container"></param>
    //    private static void RegisterTypes(UnityContainer container)
    //    {
    //        //依赖关系可以选择代码形式，也可以用配置文件的形式
    //        //UnityConfigurationSection config = (UnityConfigurationSection)ConfigurationManager.GetSection(UnityConfigurationSection.SectionName);
    //        //加载到容器
    //        //config.Configure(container, "MyContainer");
    //        container.RegisterType<IOutRecordsLayer, OutRecordsLayer>();
    //        //container.RegisterType(typeof(IUserStore<ApplicationUser>), typeof(UserStore<ApplicationUser>));
    //        container.RegisterType<AccountController>(new InjectionConstructor());
    //        container.RegisterType<ManageController>(new InjectionConstructor());

    //    }
    //}

}
