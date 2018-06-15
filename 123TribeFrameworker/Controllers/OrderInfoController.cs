using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;
using _123TribeFrameworker.Services;
using Newtonsoft.Json;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class OrderInfoController : Controller
    {
        [Dependency]
        public IOrderInfoService service { get; set; }

        // GET: OrderInfo
        public ActionResult Index(Pager<List<OrderInfo>> pager, OrderInfoQuery condition)
        {
            ViewBag.condition = condition;
            //pager = service.searchByCondition(pager, condition);
            return View(pager);
        }
        public ActionResult search(Pager<List<OrderInfo>> pager, OrderInfoQuery condition)
        {
            ViewBag.condition = condition;
            pager = service.searchByCondition(pager, condition);
            return View("Index",pager);
        }

        public async Task<ActionResult> delete(string orderNo)
        {
            var result = await service.deleteByOrderNo(orderNo);
            if (!result.result)
            {
                return View("Error", new string[] { result.message });
            }
            return RedirectToAction("search", orderNo);
        }


    }
}