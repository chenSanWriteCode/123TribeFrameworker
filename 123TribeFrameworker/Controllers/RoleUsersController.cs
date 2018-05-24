using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Models;
using Microsoft.AspNet.Identity.Owin;


namespace _123TribeFrameworker.Controllers
{
    public class RoleUsersController : Controller
    {
        public ApplicationRoleManager roleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        public ApplicationUserManager userManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET: RoleUsers
        public ActionResult Index()
        {
            IEnumerable<AplicationRole> list = new List<AplicationRole>();
            list = roleManager.Roles.ToList();
            return View(list);
        }
        [HttpPost]
        public async Task<ActionResult> delete(string id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.returnUrl = "/RoleUsers/Index";
                AplicationRole role = await roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Error", result.Errors);
                    }
                }
                else
                {
                    return View("Error", new string[] { "Not Found This Role" });
                }
            }
            return View("Error", new string[] { "Invalidate Role" });
        }

        public async Task<ActionResult> edit(string id)
        {
            if (ModelState.IsValid)
            {
                AplicationRole role = await roleManager.FindByIdAsync(id);
                string[] ids = role.Users.Select(x => x.UserId).ToArray();
                IEnumerable<ApplicationUser> members = userManager.Users.Where(x => ids.Any(y => y == x.Id));
                IEnumerable<ApplicationUser> nonMembers = userManager.Users.Except(members);
                return View(new RoleEditModel
                {
                    role = role,
                    members = members.ToList(),
                    nonMembers = nonMembers.ToList()
                });
            }
            else
            {
                ViewBag.returnUrl = "/RoleUsers/Index";
                return View("Error", new string[] { "Invalidate Role" });
            }
        }
        [HttpPost]
        public async Task<ActionResult> edit(RoleMidifiedModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model.addUsersIds ?? new string[] { })
                {
                    var result = await userManager.AddToRoleAsync(item, model.roleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach (var item in model.deleteUsersIds ?? new string[] { })
                {
                    var result = await userManager.RemoveFromRoleAsync(item, model.roleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Error",new string[] { "Invalidate Role" });
        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                AplicationRole roleModel = new AplicationRole(name);
                var result = await roleManager.CreateAsync(roleModel);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                    return View(name);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "无效Name");
                return View("");
            }
        }
    }
}