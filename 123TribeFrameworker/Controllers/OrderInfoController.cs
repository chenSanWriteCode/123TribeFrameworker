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
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public async Task<ActionResult> delete(string orderNo)
        {
            var result = await service.deleteByOrderNo(orderNo);
            if (!result.result)
            {
                ViewBag.returnUrl = "/OrderInfo/search?orderNo="+orderNo;
                return View("Error", new string[] { result.message });
            }
            else
            {
                ViewBag.returnUrl = "/OrderInfo/Index";
                ViewBag.Msg = "订单删除成功";
                return View("Success");
            }
        }


    }
}