using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Services.Layer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _123TribeFrameworker.Controllers
{
    public class HomeController : Controller
    {
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
        public ActionResult ceshi()
        {
            return View();
        }
        public ActionResult ceshi1()
        {
            return View();
        }
        public ActionResult changeCeshi1()
        {
            ViewBag.Msg = "I`m good";
            return View("ceshi1");
        }
        public string getString()
        {
            return "大家好我是主目录";
        }
        public string getSecondDirs(string ID)
        {
            DirLayer dirLayer = new DirLayer();
            JObject result = new JObject();
            string secondsDirs;
            try
            {
                secondsDirs = JsonConvert.SerializeObject(dirLayer.searchSecondDir(Convert.ToInt32(ID)));
                result.Add("menu", secondsDirs);
                result.Add("success", "true");
            }
            catch (Exception err)
            {
                result.Add("err", err.Message);
                result.Add("success", "false");
                
            }
            //string strResult = result.ToString();
            return result.ToString();
        }
        [ChildActionOnly]
        public ActionResult getMainDirs()
        {
            DirLayer dirLayer = new DirLayer();
            string htmlStr = dirLayer.searchMainDir();
            ViewBag.MainDir = Server.HtmlDecode(htmlStr?.ToString());
            return PartialView("_LoginPartial");
        }
    }
}