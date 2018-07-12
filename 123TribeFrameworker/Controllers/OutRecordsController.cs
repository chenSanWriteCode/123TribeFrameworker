using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Models.BussinessModels;
using _123TribeFrameworker.Models.QueryModel;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    [AllowAnonymous]
    public class OutRecordsController : Controller
    {
        [Dependency]
        public IOutRecordService service { get; set; }
        // GET: OutRecords
        public ActionResult Index(Pager<List<OutRecordModel>> pgaer,OutRecordQuery condition)
        {
            ViewBag.condition = condition;
            return View(pgaer);
        }
        public ActionResult search(Pager<List<OutRecordModel>> pager, OutRecordQuery condition)
        {
            ViewBag.condition = condition;
            pager = service.searchByCondition(pager, condition);
            return View("Index", pager);
        }
        public ActionResult SumIndex(Pager<List<OutRecordModel>> pgaer, OutRecordQuery condition)
        {
            ViewBag.condition = condition;
            return View(pgaer);
        }
        public ActionResult sumSearch(Pager<List<OutRecordModel>> pager, OutRecordQuery condition)
        {
            ViewBag.condition = condition;
            pager = service.searchSumByCondition(pager, condition);
            return View("SumIndex", pager);
        }
    }
}