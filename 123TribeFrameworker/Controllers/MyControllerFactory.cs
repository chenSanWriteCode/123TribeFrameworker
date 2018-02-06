//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//using Unity;

//namespace _123TribeFrameworker.Controllers
//{
//    public class MyControllerFactory : DefaultControllerFactory
//    {
//        IUnityContainer container;
//        public MyControllerFactory(IUnityContainer container)
//        {
//            this.container = container;
//        }

//        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
//        {
//            if (controllerType == null) return null;
//            return container.Resolve(controllerType) as IController;
//        }
//    }
//}