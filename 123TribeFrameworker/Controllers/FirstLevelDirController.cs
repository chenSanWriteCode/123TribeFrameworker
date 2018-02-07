using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;
using _123TribeFrameworker.Services;
using _123TribeFrameworker.Services.Layer;
using Newtonsoft.Json;

namespace _123TribeFrameworker.Controllers
{
    /// <summary>
    /// 一级菜单
    /// 具备：增删改查
    /// </summary>
    public class FirstLevelDirController : Controller
    {

        private FirstLevelDir firstLeveldir;

        //public FirstLevelDirController(IFirstLevelDir firstLevel)
        //{
        //    this.firstLevel = firstLevel;
        //}
        // GET: FirstLevelDir
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult searchFirstLevelDir(FirstLevelDirModel model,Pager page)
        {
            firstLeveldir = new FirstLevelDir();
            //var result = firstLeveldir.getFirstLevelDir(model);
            Result<List<FirstLevelDirModel>> result1 = new Result<List<FirstLevelDirModel>>();
            result1.data= firstLeveldir.getFirstLevelDir(model);
            //result1.pager = new Pager();
            //result1.pager.page = 1;
            //result1.pager.recPerPage = 20;
            //var jsonResult = Json(result1,JsonRequestBehavior.AllowGet);
            return View("Index",result1.data);
        }

        public ActionResult changeFirstLevelDir()
        {
            return RedirectToAction("Index");
        }
    }
}