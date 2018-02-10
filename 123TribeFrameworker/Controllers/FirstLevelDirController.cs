using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
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

        //private FirstLevelDir firstLeveldir;

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
            FirstLevelDir firstLeveldir = new FirstLevelDir();
            if (model != null)
            {
                pager.data = model;
            }
            Pager<List<FirstLevelDirModel>> result = firstLeveldir.getFirstLevelDir(pager);
            ViewBag.condition = model;
            return View("Index", result);
        }
        /// <summary>
        /// 修改或者增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult changeFirstLevelDir(FirstLevelDirModel condition,FirstLevelDirModel_update model_upd)
        {
            FirstLevelDir firstLeveldir = new FirstLevelDir();
            FirstLevelDirModel model = new FirstLevelDirModel(model_upd);
            Result<FirstLevelDirModel> result;
            StringBuilder sb = new StringBuilder("");
            if (model.id.HasValue)
            {
                model.lastUpdatedBy = User.Identity.Name;
                model.lastUpdatedDate = DateTime.Now;
                result = firstLeveldir.updateFirstLevelDir(model);
                sb.Append("修改");
            }
            else
            {
                model.createdBy = User.Identity.Name;
                model.createdDate = DateTime.Now;
                result = firstLeveldir.addFirstLevelDir(model);
                sb.Append("增加");
            }

            if (result.result)
            {
                TempData["Msg"] = new Message(sb.Append("成功").ToString());
            }
            else
            {
                TempData["Msg"] = new Message(sb.Append("失败").ToString());
            }
            return RedirectToAction("searchFirstLevelDir", condition);
        }
    }
}