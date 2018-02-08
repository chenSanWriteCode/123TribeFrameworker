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
        public ActionResult Index(FirstLevelDirModel model)
        {
            Pager<List<FirstLevelDirModel>> pager = new Pager<List<FirstLevelDirModel>>();
            ViewBag.condition = model;
            return View(pager);
        }
        /// <summary>
        /// search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public ActionResult searchFirstLevelDir(FirstLevelDirModel model, Pager<FirstLevelDirModel> pager)
        {
            firstLeveldir = new FirstLevelDir();
            if (model != null)
            {
                pager.data = model;
            }
            Pager<List<FirstLevelDirModel>> result = firstLeveldir.getFirstLevelDir(pager);
            ViewBag.condition = model;
            return View("Index", result);
        }

        public ActionResult changeFirstLevelDir()
        {
            return RedirectToAction("Index");
        }
    }
}