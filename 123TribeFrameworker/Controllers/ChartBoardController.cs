using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Infrastructrue;
using _123TribeFrameworker.Services;
using Newtonsoft.Json;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class ChartBoardController : Controller
    {
        [Dependency]
        public IOutRecordService outRecordServer { get; set; }

        [Dependency]
        public IOrderInfoService orderInfoServer { get; set; }
        [Dependency]
        public IInventoryService inventoryServer { get; set; }
        // GET: ChartBoard
        public async Task<ActionResult> Index()
        {
            //30天收益图
            ViewBag.MonthLabels = JsonConvert.SerializeObject(BaseDataHelper.getLastMonthDate(DateTime.Now).Select(x => x.ToString("MM月dd日")).ToArray());
            ViewBag.MonthData = JsonConvert.SerializeObject(await outRecordServer.searchLastMonthProfit());

            //昨日收益饼图
            ViewBag.YesterdayData = JsonConvert.SerializeObject(await outRecordServer.searchYesterdayNum());

            //异常订单半年线图
            ViewBag.HarfYearMonthLabel= JsonConvert.SerializeObject(BaseDataHelper.getSixMnthDate(DateTime.Now).Select(x => x.ToString("yyyy-MM")).ToArray());
            var orderData = await orderInfoServer.getHalfYearOrderNum();
            ViewBag.NormalOrderData= JsonConvert.SerializeObject(orderData.produceOrderNum);
            ViewBag.ExceptOrderData= JsonConvert.SerializeObject(orderData.exceptedOrderNum);
            ViewBag.CompletedOrderData = JsonConvert.SerializeObject(orderData.completedOrderNum);

            //库存不足柱状图
            var inventoryData =await inventoryServer.searchTenInventoryCount();
            ViewBag.InventoryLabels = JsonConvert.SerializeObject(inventoryData.materialName);
            ViewBag.InventoryAlarmCount = JsonConvert.SerializeObject(inventoryData.alarmCount);
            ViewBag.InventoryRealCount = JsonConvert.SerializeObject(inventoryData.inventoryCount);

            return View();
        }
    }
}