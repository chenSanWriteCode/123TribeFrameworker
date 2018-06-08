using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.QueryModel;
using _123TribeFrameworker.Services;
using _123TribeFrameworker.Services.Layer;
using Newtonsoft.Json;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class OrderDetailInfoController : Controller
    {
        [Dependency]
        public IOrderInfoService Orderservice { get; set; }
        [Dependency]
        public IInventoryService invnetoryService { get; set; }
        [Dependency]
        public OrderDetailInfoService detailService { get; set; }
        // GET: OrderDetailInfo
        public ActionResult Index(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery condition)
        {
            ViewBag.condition = condition;
            return View(pager);
        }
        public ActionResult search(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery condition)
        {
            pager = detailService.searchByCondition(pager, condition);
            ViewBag.condition = condition;
            return View("Index",pager);
        }
        //public JsonResult search(Pager<List<OrderDetailInfo>> pager, OrderDetailInfo condition)
        //{
        //    pager = detailService.searchByCondition(pager, condition);
        //    ViewBag.condition = condition;
        //    DataGridResult<List<OrderDetailInfo>> result = new DataGridResult<List<OrderDetailInfo>>();
        //    result.data = pager.data;
        //    result.pager.page = pager.page;
        //    result.pager.recPerPage = pager.recPerPage;
        //    result.pager.recTotal = pager.recTotal;
        //    var jsonResult = Json(result);
        //    return Json(result);
        //}


        #region 创建订单


        public ActionResult add(Pager<List<Inventory>> pager)
        {
            pager = new Pager<List<Inventory>>(50);
            pager = invnetoryService.searchByNumOrder(pager);
            return View(pager);
        }
        [HttpPost]
        public async Task<ActionResult> generateOrder(string jsonOrder)
        {
            ViewBag.returnUrl = "/OrderDetailInfo/Index";
            var orderList = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(jsonOrder);
            var priceList = JsonConvert.DeserializeObject<List<PriceCount>>(jsonOrder);
            OrderInfo order = new OrderInfo();
            order.createdBy = User.Identity.Name;
            order.orderNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            order.status = "generated";
            order.sumPrice = priceList.Sum(x=>x.referencePriceIn*x.num);
            
            foreach (var item in orderList)
            {
                item.orderNo = order.orderNo;
                item.createdBy = order.createdBy;
            }
            var orderResult = await Orderservice.addAsync(order);
            var detailResult = await detailService.addRange(orderList);
            if (detailResult.result)
            {
                ViewBag.Msg = "订单生成成功";
                return View("Success");
            }
            return View("Error", new string[] { detailResult.message });
        }
    }
    #endregion
   public class PriceCount
    {
        public int num { get; set; }
        public double referencePriceIn { get; set; }
    }
}