using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Services.Layer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _123TribeFrameworker.Controllers
{
    public class SecondLevelDirController : Controller
    {
        public ActionResult Index(SecondLevelDirModel model)
        {
            Pager<List<SecondLevelDirModel>> pager = new Pager<List<SecondLevelDirModel>>();
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
            SecondLevelDir secondLeveldir = new SecondLevelDir();
            if (model != null)
            {
                pager.data = model;
            }
            Pager<List<SecondLevelDirModel>> result = secondLeveldir.getSecondLevelDir(pager);
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
            SecondLevelDir secondLeveldir = new SecondLevelDir();
            SecondLevelDirModel model = new SecondLevelDirModel(model_upd);
            Result<SecondLevelDirModel> result;
            StringBuilder sb = new StringBuilder("");
            if (model.id.HasValue)
            {
                model.lastUpdatedBy = User.Identity.Name;
                model.lastUpdatedDate = DateTime.Now;
                result = secondLeveldir.updateSecondLevelDir(model);
                sb.Append("修改");
            }
            else
            {
                model.createdBy = User.Identity.Name;
                model.createdDate = DateTime.Now;
                result = secondLeveldir.addSecondLevelDir(model);
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
            SecondLevelDir layer = new SecondLevelDir();
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
                SecondLevelDir layer = new SecondLevelDir();
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
            SecondLevelDir layer = new SecondLevelDir();
            SecondLevelDirModel model = layer.getSingleSecondDir(id);
            return JsonConvert.SerializeObject(model);
        }

        public string ceshi()
        {
            SecondLevelDir layer = new SecondLevelDir();
            JObject ob = new JObject();
            SecondLevelDirModel model = layer.getSingleSecondDir(1);
            string json = JsonConvert.SerializeObject(model);
            ob.Add("data", json);
            string result = ob.ToString();
            return result;
        }
    }
}