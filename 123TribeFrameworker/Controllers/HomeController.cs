using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _123TribeFrameworker.Infrastructrue;
using _123TribeFrameworker.Services;
using _123TribeFrameworker.Services.Layer;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IDirLayerService dirLayer { get; set; }
        public ApplicationRoleManager roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [OutputCache(Duration =120,VaryByParam ="ID")]
        public JsonResult getSecondDirs(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                ID = "4";
            }
            var result  = Json(dirLayer.searchSecondDir(getCurrentRoleId(), Convert.ToInt32(ID)));
            return result;
        }
        [ChildActionOnly]
        [OutputCache(Duration = 120 ,VaryByParam = "none")]
        public ActionResult getMainDirs()
        {
            string htmlStr = dirLayer.searchMainDir(getCurrentRoleId());
            ViewBag.MainDir = Server.HtmlDecode(htmlStr?.ToString());
            return PartialView("_LoginPartial");
        }

        public ActionResult returnBack(string returnUrl)
        {
            return Redirect(returnUrl);
        }
        public string getCurrentRoleId()
        {
            Dictionary<string, string> dict = roleManager.Roles.ToDictionary(x => x.Id, x => x.Name);
            if (dict != null)
            {
                foreach (var item in dict)
                {
                    if (User.IsInRole(item.Value))
                    {
                        return item.Key;
                    }
                }
            }
            return "";
        }
    }
}