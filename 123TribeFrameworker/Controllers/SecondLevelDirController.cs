using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Models.QueryModel;
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
        public ActionResult Index(Pager<List<SecondLevel>> pager,SecondMenuQuery model)
        {
            ViewBag.Condition = model;
            return View(pager);
        }
        /// <summary>
        /// search
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public async Task<ActionResult> searchSecondLevelDir(Pager<List<SecondLevel>> pager, SecondMenuQuery condition)
        {
            ViewBag.Condition = condition;
            var result = await layer.getSecondLevelDir(pager, condition);
            if (result.success)
            {
                return View("Index", pager);
            }
            else
            {
                ViewBag.ReturnUrl = "/SecondLevelDir/Index";
                ViewBag.Msg = result.message;
                return View("Error");
            }
        }
        public async Task<ActionResult> Update(int id)
        {
            if (id>0)
            {
                var result =await layer.getSingleSecondDir(id);
                if (result.success)
                {
                    return View(result.data);
                }
                else
                {
                    ViewBag.Msg = result.message;
                }
            }
            else
            {
                ViewBag.Msg = "出现不可预期的异常，请重新操作";
            }
            ViewBag.ReturnUrl = "/SecondLevelDir/Index";
            return View("Error");
        }
        [HttpPost]
        public async Task<ActionResult> Update(SecondLevelDirModel model)
        {
            if (ModelState.IsValid)
            {
                var result =await layer.updateSecondLevelDir(model, User.Identity.Name);
                if (result.success)
                {
                    ViewBag.ReturnUrl = "/SecondLevelDir/Update?id=" + model.id;
                    return View("Success");
                }
                else
                {
                    ModelState.AddModelError("", result.message);
                }
            }
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Add(SecondLevelDirModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await layer.addSecondLevelDir(model, User.Identity.Name);
                if (result.success)
                {
                    ViewBag.ReturnUrl = "/SecondLevelDir/Add";
                    return View("Success");
                }
                else
                {
                    ModelState.AddModelError("", result.message);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await layer.deleteSecondLevelDir(id);
            ViewBag.ReturnUrl = "/SecondLevelDir/Index";
            if (result.success)
            {
                return View("Success");
            }
            else
            {
                ViewBag.Msg = result.message;
                return View("Error");
            }
        }
    }
}