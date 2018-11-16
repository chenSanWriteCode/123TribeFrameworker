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
using Newtonsoft.Json;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    public class SaleController : Controller
    {
        [Dependency]
        public ISaleService service { get; set; }
        [Dependency]
        public IMaterialInfoService materialService { get; set; }
        [Dependency]
        public IOutRecordService outRecordService { get; set; }
        [Dependency]
        public IInventoryService inventoryService { get; set; }

        // GET: Sale
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pager">所有物料</param>
        /// <param name="jsonStr">卖出货物的json字符串</param>
        /// <returns></returns>
        public async Task<ActionResult> Index(string jsonStr)
        {
            //客户选择的订单 or 空
            ViewBag.orderData = string.IsNullOrEmpty(jsonStr)?"empty":jsonStr;
            //前十热销榜
            List<MaterialInfo> hotMaterial = new List<MaterialInfo>();
            hotMaterial = outRecordService.searchHotTen();
            ViewBag.hotMaterial = hotMaterial;
            //产品
            var list = await materialService.searchListByCondition(new MaterialInfoQuery());
            var findIndexs = list.Select(x => x.findIndx).Distinct().ToList();
            ViewBag.Indexs = findIndexs;
            //库存报警
            ViewBag.warningInventory = await inventoryService.searchTenLackInventory();
            return View(list);
        }
        public async Task<ActionResult> sale(string jsonStr)
        {
            List<SaleModel> list = JsonConvert.DeserializeObject<List<SaleModel>>(jsonStr);
            var result = await service.receive(list, User.Identity.Name);
            if (result.success)
            {
                ViewBag.Msg = "收银成功";
                ViewBag.returnUrl = "/Sale/Index";
                return View("Success");
            }
            else
            {
                ViewBag.returnUrl = "/sale/Index?jsonStr="+jsonStr;
                return View("Error", new string[] { "收银失败：" + result.message });
            }
        }

    }
}