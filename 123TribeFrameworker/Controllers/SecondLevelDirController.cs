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
    }
}