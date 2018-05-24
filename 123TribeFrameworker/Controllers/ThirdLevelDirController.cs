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
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    
    public class ThirdLevelDirController : Controller
    {
        [Dependency]
        public IThirdLevelDirService layer { get; set; }
        public ActionResult Index(ThirdLevelDirModel model)
        {
            Pager<List<ThirdLevelDirModel>> pager = new Pager<List<ThirdLevelDirModel>>();
            if (!model.secondLevelID.HasValue)
            {
                model.secondLevelID = 1;
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
        public ActionResult searchThirdLevelDir(ThirdLevelDirModel model, Pager<ThirdLevelDirModel> pager)
        {
            if (model != null)
            {
                pager.data = model;
            }
            
            Pager<List<ThirdLevelDirModel>> result = layer.getThirdLevelDir(pager);
            ViewBag.condition = model;
            return View("Index", result);
        }
        /// <summary>
        /// 修改或者增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult changeThirdLevelDir(ThirdLevelDirModel condition, ThirdLevelDirModel_update model_upd)
        {
            ThirdLevelDirModel model = new ThirdLevelDirModel(model_upd);
            Result<ThirdLevelDirModel> result;
            StringBuilder sb = new StringBuilder("");
            if (model.id.HasValue)
            {
                model.lastUpdatedBy = User.Identity.Name;
                model.lastUpdatedDate = DateTime.Now;
                result = layer.updateThirdLevelDir(model);
                sb.Append("修改");
            }
            else
            {
                model.createdBy = User.Identity.Name;
                model.createdDate = DateTime.Now;
                result = layer.addThirdLevelDir(model);
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
           // condition.secondLevelID = model_upd.secondLevelID_upd;
            return RedirectToAction("searchThirdLevelDir", condition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="model_upd"></param>
        /// <returns></returns>
        public ActionResult deleteThirdLevelDir(ThirdLevelDirModel condition, ThirdLevelDirModel_update model_upd)
        {
            if (model_upd.id_upd.HasValue)
            {
                StringBuilder sb = new StringBuilder("删除");
                Result<ThirdLevelDirModel> result = layer.deleteThirdLevelDir(model_upd.id_upd.Value);
                if (result.result)
                {
                    TempData["Msg"] = new Message(sb.Append("成功").ToString());
                }
                else
                {
                    TempData["Msg"] = new Message(sb.Append("失败").ToString());
                }
            }
            return RedirectToAction("searchThirdLevelDir", condition);
        }
        /// <summary>
        /// 根据id获取一条3级菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getSingleThirdDir(int id)
        {
            ThirdLevelDirModel model = layer.getSingleThirdDir(id);
            return JsonConvert.SerializeObject(model);
        }
    }
}