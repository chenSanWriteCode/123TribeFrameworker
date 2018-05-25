using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{

    
    public class OrderInfoController : Controller
    {
        [Dependency]
        public IOrderInfoService service { get; set; }
        // GET: OrderInfo
        public ActionResult Index(Pager<List<OrderInfo>> pager,OrderInfo condition)
        {
            ViewBag.condition = condition;
            pager = service.searchByCondition(pager, condition);
            return View(pager);
        }
        public async Task<ActionResult> add()
        {
            OrderDetailInfo model = new OrderDetailInfo();
            string orderNo = DateTime.Now.ToString("yyyyMMddHHmm");
            OrderInfo orderModel = await service.searchByOrder(orderNo);
            if (orderModel!=null)
            {

            }
            model.orderNo = DateTime.Now.ToString("yyyyMMddHHmm");
            return View(model);
        }
        [HttpPost]
        public ActionResult add(List<OrderDetailInfo> list)
        {
            return RedirectToAction("Index");
        }
    }
}