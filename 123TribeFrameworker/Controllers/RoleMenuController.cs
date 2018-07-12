using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.CommonTools;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models;
using _123TribeFrameworker.Models.DirModels;
using _123TribeFrameworker.Services;
using _123TribeFrameworker.Services.Layer;
using Microsoft.AspNet.Identity.Owin;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class RoleMenuController : Controller
    {
        [Dependency]
        public IRoleMenuLayerService layer { get; set; }
        public ApplicationRoleManager roleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }
        // GET: RoleMenu
        public ActionResult Index(string id = null)
        {
            return View(layer.searchRoleMenusAsync(id));
        }
        /// <summary>
        /// viewBag contains
        /// 1. role list
        /// 2. firstMenu in role
        /// 3. secondMenu in role
        /// 4. thirdMenu in role
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> addMenu(string roleId=null)
        {
            Dictionary<string, string> dic = roleManager.Roles.ToDictionary(x => x.Id, x => x.Name);
            SelectList selectList = new SelectList(dic, "Key", "Value", 0);
            ViewBag.DropDown = selectList;

            roleId = string.IsNullOrEmpty(roleId)? selectList.First().Value:roleId;
            RoleMenuEdit roleMenus = new RoleMenuEdit
            {
                role = await roleManager.FindByIdAsync(roleId),
                firstDirs = layer.searchRoleMenusNotInRoleId(roleId, DirLevel.FirstLevel),
                secondDirs = layer.searchRoleMenusNotInRoleId(roleId, DirLevel.SecondLevel),
            };
            return View(roleMenus);
        }

        [HttpPost]
        public async Task<ActionResult> addMenu(RoleMenuModified model)
        {
            ViewBag.returnUrl = "/RoleMenu/Index";
            if (ModelState.IsValid)
            {
                Result<RoleMenu> resultF = new Result<RoleMenu>();
                Result<RoleMenu> resultS = new Result<RoleMenu>();
                Result<RoleMenu> resultT = new Result<RoleMenu>();
                if (model.firstDirIds != null && model.firstDirIds.Count() > 0)
                {
                    resultF = await layer.addRoleMenuRangeAsync(model.roleId, DirLevel.FirstLevel, model.firstDirIds);
                }
                if (model.secondDirIds != null && model.secondDirIds.Count() > 0)
                {
                    resultS = await layer.addRoleMenuRangeAsync(model.roleId, DirLevel.SecondLevel, model.secondDirIds);
                }
                var result = combineResults<RoleMenu>(resultF, resultS, resultT);
                if (result.result)
                {
                    return RedirectToAction("Index","RoleMenu", model.roleId);
                }
                else
                {
                    return View("Error", new string[] { result.message });
                }
            }
            else
            {
                return View("Error", new string[] { "Invalid data" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> deleteMenu(int id)
        {
            ViewBag.returnUrl = "/RoleMenu/Index";
            if (ModelState.IsValid)
            {
                Result<RoleMenu> result = await layer.deleteMenuAsync(id);
                if (!result.result)
                {
                    return View("Error", new string[] { result.message });
                }
                else
                {
                    var model = await layer.searchRoleMenuByIdAsync(id);
                    if (model != null)
                    {
                        return RedirectToAction("Index","RoleMenu", model.roleId);
                    }
                    else
                    {
                        return View("Error", new string[] { "Role Is Not Valid" });
                    }
                }
            }
            else
            {
                return View("Error", new string[] { "Role Is Not Valid" });
            }
        }

        #region tool

        /// <summary>
        /// 整合 result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public Result<T> combineResults<T>(params Result<T>[] results)
        {
            Result<T> result = new Result<T>();
            foreach (var item in results)
            {
                if (!item.result)
                {
                    result.result = false;
                    result.message = result.message + item.message + "; ";
                }
            }
            return result;
        }
        #endregion
    }
}