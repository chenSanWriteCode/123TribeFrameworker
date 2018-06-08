using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class InventoryController : Controller
    {
        [Dependency]
        public IInventoryService service { get; set; }
        // GET: Inventory
        public ActionResult Index(Pager<List<Inventory>> pager,MaterialInfoQuery condition)
        {
            ViewBag.condition = condition;
            var data = Json(pager.data);
            Pager<JsonResult> pager1 = new Pager<JsonResult>();
            pager1.page = pager.page;
            pager1.recPerPage = pager.recPerPage;
            pager1.recTotal = pager.recTotal;
            pager1.data = data;
            return View(pager1);
        }
        public JsonResult search(Pager<List<Inventory>> pager, MaterialInfo condition)
        {
            InventoryQuery model = new InventoryQuery();
            pager = service.searchByCondition(pager, model);
            ViewBag.condition = condition;
            var data = Json(pager.data);
            return data;
        }
    }
}