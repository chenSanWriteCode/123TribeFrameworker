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
    public class InStorageRecordController : Controller
    {
        [Dependency]
        public IInStorageRecordService service { get; set; }
        // GET: InStorageRecord
        public ActionResult Index(Pager<List<InStorageRecord>> pager ,InStorageRecordQuery condition)
        {
            ViewBag.condition = condition;
            return View(pager);
        }
        [HttpPost]
        public async Task<ActionResult> search(Pager<List<InStorageRecord>> pager, InStorageRecordQuery condition)
        {
            pager = await service.searchByCondition(pager, condition);
            ViewBag.condition = condition;
            return View("Index", pager);
        }
        #region 入库汇总查询
        public ActionResult SumIndex(Pager<List<InStorageRecordModel>> pager, InStorageRecordQuery condition)
        {
            ViewBag.condition = condition;
            return View(pager);
        }
        [HttpPost]
        public async Task<ActionResult> SumSearch(Pager<List<InStorageRecordModel>> pager, InStorageRecordQuery condition)
        {
            pager = await service.searchSumByCondition(pager, condition);
            ViewBag.condition = condition;
            return View("SumIndex", pager);
        }
        #endregion

    }
}