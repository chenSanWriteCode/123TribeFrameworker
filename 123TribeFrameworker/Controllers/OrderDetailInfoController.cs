using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
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
        [Dependency]
        public IInStorageRecordService inStroageService { get; set; }

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
            return View("Index", pager);
        }

        #region 订单修改
        public ActionResult update(string orderNo)
        {
            OrderDetailInfoQuery condition = new OrderDetailInfoQuery()
            {
                orderNo = orderNo,
                status = "generated"
            };
            var list = detailService.searchAllByCondition(condition);
            if (list.Count==0)
            {
                ViewBag.returnUrl = "/OrderInfo/search?orderNo="+orderNo;
                return View("Error", new string[] { "未收货订单中查询不到" + orderNo + "，请检查订单是否已收货完成或取消" });
            }
            return View(list);
        }
        /// <summary>
        /// 删除详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> deleteDetail(int id)
        {
            var result = await detailService.deleteById(id,User.Identity.Name);
            ViewBag.returnUrl = "/OrderDetailInfo/update?orderNo=" + result.data.orderNo;
            if (result.result)
            {
                ViewBag.Msg = "删除成功";
                return View("Success");
            }
            return View("Error", new string[] { result.message });
        }
        /// <summary>
        ///修改详细
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public async Task<ActionResult> updateDetail(int id,string orderNo)
        {
            var model = await detailService.searchById(id);
            if (model==null)
            {
                ViewBag.returnUrl = "/OrderDetailInfo/update?orderNo="+ orderNo;
                return View("Error", new string[] { "订单已被删除" });
            }
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> updateDetail(OrderDetailInfo model)
        {
            model.lastUpdatedBy = User.Identity.Name;
            model.lastUpdatedDate = DateTime.Now;
            var result = await detailService.updateDetail(model);
            if (!result.result)
            {
                ViewBag.returnUrl = "/OrderDetailInfo/updateDetail?id=" + model.id+"&orderNo="+model.orderNo;
                return View("Error",new string[] { "操作失败："+result.message});
            }
            ViewBag.returnUrl = "/OrderDetailInfo/update?orderNo=" + model.orderNo;
            ViewBag.Msg = "修改成功";
            return View("Success");
        }
        /// <summary>
        /// 增加详细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult addDetail([Required]string orderNo)
        {
            ViewBag.orderNo = orderNo;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> addDetail(OrderDetailInfo model)
        {
            model.createdBy = User.Identity.Name;
            if (model.num <= 0)
            {
                ModelState.AddModelError("", "数量必须为正数");
                ViewBag.orderNo = model.orderNo;
                return View();
            }
            var result = await detailService.addDetail(model);
            if (!result.result)
            {
                ModelState.AddModelError("",result.message);
                ViewBag.orderNo = model.orderNo;
                return View();
            }
            ViewBag.returnUrl = "/OrderDetailInfo/update?orderNo="+model.orderNo;
            ViewBag.Msg = "增加成功";
            return View("Success");
        }
        #endregion

        #region 订单收货
        /// <summary>
        /// 订单收货
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult receive(string orderNo)
        {
            OrderDetailInfoQuery condition = new OrderDetailInfoQuery();
            condition.orderNo = orderNo;
            condition.status = "generated";
            var result = detailService.searchAllByCondition(condition);
            if (result.Count==0)
            {
                ViewBag.returnUrl = "/OrderInfo/Index";
                return View("Error", new string[] { "未收货订单中查询不到"+orderNo + "，请检查订单是否已收货完成或取消" });
            }
            return View(result);
        }
        /// <summary>
        /// 增加库存收货
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> receive(List<InStorageRecord> list, [Required]string receivedOrder)
        {
            var orderNo = list.First().orderNo;
            ViewBag.returnUrl = "/OrderDetailInfo/receive?orderNo="+orderNo;
            var countCheck = list.Where(x => x.countReal <= 0 ).Count();
            if (countCheck > 0)
            {
                return View("Error",new string[] { "实收数量必须为正数" });
            }
            var priceCheck = list.Where(x => x.priceIn <= 0).Count();
            if (priceCheck>0)
            {
                return View("Error", new string[] { "实际进价必须为正数" });
            }
            
            Result<int> result = await detailService.receiveOrder(list, receivedOrder, User.Identity.Name);
            if (result.result)
            {
                ViewBag.returnUrl = "/OrderInfo/search?orderNo="+orderNo;
                ViewBag.Msg = "订单收货成功！";
                return View("Success");
            }
            return View("Error", new string[] { result.message });
        }
        #endregion

        #region 创建订单

        public async Task<ActionResult> add(Pager<List<InventorySimpleModel>> pager)
        {
            pager = new Pager<List<InventorySimpleModel>>(50);
            pager = await invnetoryService.searchByNumOrder(pager);
            return View(pager);
        }
        [HttpPost]
        public async Task<ActionResult> generateOrder(string jsonOrder)
        {
            ViewBag.returnUrl = "/OrderDetailInfo/Index";
            var orderList = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(jsonOrder);
            var priceList = JsonConvert.DeserializeObject<List<PriceCount>>(jsonOrder);
            var sumPrice = priceList.Sum(x => x.referencePriceIn * x.num);
            
            var orderResult = await Orderservice.addOrder(orderList, User.Identity.Name, sumPrice);
            if (orderResult.result)
            {
                ViewBag.Msg = "订单生成成功:" + orderResult.data.First().orderNo;
                return View("Success");
            }
            return View("Error", new string[] { orderResult.message });
        }
        #endregion

        #region 处理异常订单
        public ActionResult dealReceivedOrder(string orderNo)
        {
            InStorageRecordQuery condition = new InStorageRecordQuery();
            condition.orderNo = orderNo;
            var instorageRecords = inStroageService.searchAllByCondition(condition);
            if (instorageRecords.Count==0)
            {
                ViewBag.returnUrl = "/OrderInfo/search?orderNo=" + orderNo;
                return View("Error", new string[] { "订单已被处理" });
            }
            return View(instorageRecords);
        }
        [HttpPost]
        public async Task<ActionResult> dealReceivedOrder(List<InStorageRecord> list)
        {
            var orderNo = list.First().orderNo;
            ViewBag.returnUrl = "/OrderDetailInfo/dealReceivedOrder?orderNo=" + orderNo;
            var countCheck = list.Where(x => x.countReal < 0).Count();
            if (countCheck > 0)
            {
                return View("Error", new string[] { "实收数量必须为正数" });
            }
            Result<int> result = await detailService.dealReceivedOrder(list, User.Identity.Name);
            if (result.result)
            {
                ViewBag.returnUrl = "/OrderInfo/Index?orderNo=" + orderNo;
                ViewBag.Msg = "订单异常处理成功！";
                return View("Success");
            }
            return View("Error", new string[] { result.message });
        }
        #endregion
    }


    public class PriceCount
    {
        public int num { get; set; }
        public float referencePriceIn { get; set; }
    }
}