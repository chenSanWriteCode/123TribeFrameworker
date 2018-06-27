using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
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
        public ActionResult Index(Pager<List<InventorySimpleModel>> pager, InventoryQuery condition)
        {
            ViewBag.condition = condition;
            return View(pager);
        }
        [HttpPost]
        public async Task<ActionResult> search(Pager<List<InventorySimpleModel>> pager, InventoryQuery condition)
        {
            pager = await service.searchByCondition(pager, condition);
            ViewBag.condition = condition;
            return View("Index", pager);
        }
    }
}