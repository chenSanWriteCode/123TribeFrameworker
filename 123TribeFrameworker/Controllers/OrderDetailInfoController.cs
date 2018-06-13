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
        public IOrderDetailInfoService detailService { get; set; }
        
        // GET: OrderDetailInfo
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Index(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery condition)
        {
            ViewBag.condition = condition;
            return View(pager);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult search(Pager<List<OrderDetailInfo>> pager, OrderDetailInfoQuery condition)
        {
            pager = detailService.searchByCondition(pager, condition);
            ViewBag.condition = condition;
            return View("Index",pager);
        }
        
        /// <summary>
        /// 订单收货
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult receive(string orderNo)
        {
            OrderDetailInfoQuery condition = new OrderDetailInfoQuery();
            condition.orderNo = orderNo;
            var result = detailService.searchAllByCondition(condition);
            return View(result);
        }
        /// <summary>
        /// 增加库存收货
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> receiveOrder(List<InStorageRecord> list)
        {
            if (ModelState.IsValid)
            {
                Result<int> result = await detailService.receiveOrder(list,User.Identity.Name);
                if (result.result)
                {
                    ViewBag.returnUrl = "/OrderInfo/Index";
                    ViewBag.Msg = "订单收货成功！";
                    return View("Success");
                }
                else
                {
                    ModelState.AddModelError("", result.message);
                }
            }
            return View();
        }

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
                ViewBag.Msg = "订单生成成功:"+order.orderNo;
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