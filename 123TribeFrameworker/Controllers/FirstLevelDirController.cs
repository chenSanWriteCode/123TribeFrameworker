using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Services.Layer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        /// <summary>
        /// 返回一级菜单列表
        /// </summary>
        /// <returns></returns>
        public string getFirstLevelDirList()
        {
            FirstLevelDir layer = new FirstLevelDir();
            //JObject json = new JObject();
            Dictionary<int, string> firstDirDict = layer.getFirstLevelDirDict();
            //json.Add(firstDirDict);
            string jsonStr = JsonConvert.SerializeObject(firstDirDict);
            return jsonStr;
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="model_upd"></param>
        /// <returns></returns>
        public ActionResult deleteFirstLevelDir(FirstLevelDirModel condition, FirstLevelDirModel_update model_upd)
        {
            if (model_upd.id_upd.HasValue)
            {
                FirstLevelDir layer = new FirstLevelDir();
                StringBuilder sb = new StringBuilder("删除");
                Result<FirstLevelDirModel> result = layer.deleteFirstLevelDir(model_upd.id_upd.Value);
                if (result.result)
                {
                    TempData["Msg"] = new Message(sb.Append("成功").ToString());
                }
                else
                {
                    TempData["Msg"] = new Message(sb.Append("失败").ToString());
                }
            }
            return RedirectToAction("searchFirstLevelDir", condition);
        }
        /// <summary>
        /// 根据id获取一条主菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public string getSingleFirstDir(int id)
        //{
        //    FirstLevelDir layer = new FirstLevelDir();
        //    FirstLevelDirModel model = layer.getSingleSecondDir(id);
        //    return JsonConvert.SerializeObject(model);
        //}
    }
}