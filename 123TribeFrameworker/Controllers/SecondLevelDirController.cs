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
using Newtonsoft.Json.Linq;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class SecondLevelDirController : Controller
    {
        [Dependency]
        public ISecondLevelDirService layer { get; set; }
        public ActionResult Index(SecondLevelDirModel model)
        {
            Pager<List<SecondLevelDirModel>> pager = new Pager<List<SecondLevelDirModel>>();
            if (!model.firstLevelID.HasValue)
            {
                model.firstLevelID = 1;
            }
            ViewBag.condition = model;
            return View(pager);
        }
        /// <summary>
        /// search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public ActionResult searchSecondLevelDir(SecondLevelDirModel model, Pager<SecondLevelDirModel> pager)
        {
            if (model != null)
            {
                pager.data = model;
                
            }
            Pager<List<SecondLevelDirModel>> result = layer.getSecondLevelDir(pager);
            ViewBag.condition = model;
            return View("Index", result);
        }
        /// <summary>
        /// 修改或者增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult changeSecondLevelDir(SecondLevelDirModel condition, SecondLevelDirModel_update model_upd)
        {
            SecondLevelDirModel model = new SecondLevelDirModel(model_upd);
            Result<SecondLevelDirModel> result;
            StringBuilder sb = new StringBuilder("");
            if (model.id.HasValue)
            {
                model.lastUpdatedBy = User.Identity.Name;
                model.lastUpdatedDate = DateTime.Now;
                result = layer.updateSecondLevelDir(model);
                sb.Append("修改");
            }
            else
            {
                model.createdBy = User.Identity.Name;
                model.createdDate = DateTime.Now;
                result = layer.addSecondLevelDir(model);
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
            return RedirectToAction("searchSecondLevelDir", condition);
        }

        /// <summary>
        /// 返回2级菜单列表
        /// </summary>
        /// <returns></returns>
        public string getSecondLevelDirList()
        {
            Dictionary<int, string> secondDirDict = layer.getSecondLevelDirDict();
            return JsonConvert.SerializeObject(secondDirDict);
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="model_upd"></param>
        /// <returns></returns>
        public ActionResult deleteSecondLevelDir(SecondLevelDirModel condition, SecondLevelDirModel_update model_upd)
        {
            if (model_upd.id_upd.HasValue)
            {
                StringBuilder sb = new StringBuilder("删除");
                Result<SecondLevelDirModel> result = layer.deleteSecondLevelDir(model_upd.id_upd.Value);
                if (result.result)
                {
                    TempData["Msg"] = new Message(sb.Append("成功").ToString());
                }
                else
                {
                    TempData["Msg"] = new Message(sb.Append("失败").ToString());
                }
            }
            return RedirectToAction("searchSecondLevelDir", condition);
        }
        /// <summary>
        /// 根据id获取一条二级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getSingleSecondDir(int id)
        {
            SecondLevelDirModel model = layer.getSingleSecondDir(id);
            return JsonConvert.SerializeObject(model);
        }
    }
}