using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Services.Layer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    /// <summary>
    /// 一级菜单
    /// 具备：增删改查
    /// </summary>
    public class FirstLevelDirController : Controller
    {
        [Dependency]
        public FirstLevelDirServiceImpl layer{ get; set; }
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
            if (model != null)
            {
                pager.data = model;
            }
            Pager<List<FirstLevelDirModel>> result = layer.getFirstLevelDir(pager);
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
            FirstLevelDirModel model = new FirstLevelDirModel(model_upd);
            Result<FirstLevelDirModel> result;
            StringBuilder sb = new StringBuilder("");
            if (model.id.HasValue)
            {
                model.lastUpdatedBy = User.Identity.Name;
                model.lastUpdatedDate = DateTime.Now;
                result = layer.updateFirstLevelDir(model);
                sb.Append("修改");
            }
            else
            {
                model.createdBy = User.Identity.Name;
                model.createdDate = DateTime.Now;
                result = layer.addFirstLevelDir(model);
                sb.Append("增加");
            }

            if (result.success)
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
        /// delete
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="model_upd"></param>
        /// <returns></returns>
        public ActionResult deleteFirstLevelDir(FirstLevelDirModel condition, FirstLevelDirModel_update model_upd)
        {
            if (model_upd.id_upd.HasValue)
            {
                StringBuilder sb = new StringBuilder("删除");
                Result<FirstLevelDirModel> result = layer.deleteFirstLevelDir(model_upd.id_upd.Value);
                if (result.success)
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
        //[HttpPost]
        //public ActionResult delete(FirstLevelDirModel model, Pager<FirstLevelDirModel> pager,string id)
        //{
        //    ViewBag.retrunURL = "searchFirstLevelDir";
        //    if (ModelState.IsValid)
        //    {
        //        StringBuilder sb = new StringBuilder("删除");
        //        Result<FirstLevelDirModel> result = layer.deleteFirstLevelDir(Convert.ToInt32(id));
        //        if (result.result)
        //        {
        //            return RedirectToAction("searchFirstLevelDir",new { model,pager});
        //        }
        //        else
        //        {
        //            return View("Error", new string[] { result.message });
        //        }
        //    }
        //    else
        //    {
        //        return View("Error", new string[] { "无效的菜单" });
        //    }
        //}
        #region tool
        /// <summary>
        /// 返回一级菜单列表
        /// </summary>
        /// <returns></returns>
        public string getFirstLevelDirList()
        {
            //JObject json = new JObject();
            Dictionary<int, string> firstDirDict = layer.getFirstLevelDirDict();
            //json.Add(firstDirDict);
            string jsonStr = JsonConvert.SerializeObject(firstDirDict);
            return jsonStr;
        }
        /// <summary>
        /// 根据id获取一条主菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public string getSingleFirstDir(int id)
        //{
        //    FirstLevelDirModel model = layer.getSingleSecondDir(id);
        //    return JsonConvert.SerializeObject(model);
        //}
        #endregion
    }
}