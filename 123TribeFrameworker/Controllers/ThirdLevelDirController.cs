using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Services.Layer;

namespace _123TribeFrameworker.Controllers
{
    public class ThirdLevelDirController : Controller
    {
        public ActionResult Index(ThirdLevelDirModel model)
        {
            Pager<List<ThirdLevelDirModel>> pager = new Pager<List<ThirdLevelDirModel>>();
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
            ThirdLevelDir firstLeveldir = new ThirdLevelDir();
            if (model != null)
            {
                pager.data = model;
            }
            Pager<List<ThirdLevelDirModel>> result = firstLeveldir.getThirdLevelDir(pager);
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
            ThirdLevelDir firstLeveldir = new ThirdLevelDir();
            ThirdLevelDirModel model = new ThirdLevelDirModel(model_upd);
            Result<ThirdLevelDirModel> result;
            StringBuilder sb = new StringBuilder("");
            if (model.id.HasValue)
            {
                model.lastUpdatedBy = User.Identity.Name;
                model.lastUpdatedDate = DateTime.Now;
                result = firstLeveldir.updateThirdLevelDir(model);
                sb.Append("修改");
            }
            else
            {
                model.createdBy = User.Identity.Name;
                model.createdDate = DateTime.Now;
                result = firstLeveldir.addThirdLevelDir(model);
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
            return RedirectToAction("searchThirdLevelDir", condition);
        }
    }
}