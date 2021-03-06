﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class MaterialInfoController : Controller
    {
        [Dependency]
        public IMaterialInfoService service { get; set; }
        // GET: MaterialInfo
        public ActionResult Index(Pager<List<MaterialInfo>> pager, MaterialInfoQuery condition)
        {
            ViewBag.condition = condition;
            pager = service.searchByCondition(pager, condition);
            return View(pager);
        }
        public ActionResult add()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> add(MaterialInfo model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.addAsync(model);
                if (!result.success)
                {
                    ModelState.AddModelError("", result.message);
                }
                else
                {
                    ModelState.AddModelError("", "保存成功");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> edit(int id)
        {
            var model = await service.searchByid(id);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> edit(MaterialInfo model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.update(model);
                if (!result.success)
                {
                    ModelState.AddModelError("", result.message);
                }
                else
                {
                    ModelState.AddModelError("", "保存成功");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> delete(int id,MaterialInfo condition)
        {
            var result = await service.deleteByIdAsync(id);
            if (!result.success)
            {
                return View("Error", new string[] { result.message });
            }
            return RedirectToAction("Index", condition);
        }

        public ActionResult selectList()
        {
            return View();
        }
        public JsonResult list(MaterialInfoQuery condition)
        {
            ViewBag.condition = condition;
            Pager<List<MaterialInfo>> pager = new Pager<List<MaterialInfo>>();
            pager = service.searchByCondition(pager, condition);
            JsonResult result = Json(pager.data);
            return result;
        }

        public async Task<JsonResult> searchModel(int id)
        {
            var result = await service.searchByid(Convert.ToInt32(id));
            return Json(result);
        }
    }
}