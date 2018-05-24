using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _123TribeFrameworker.Entity;
using _123TribeFrameworker.Services;
using Unity.Attributes;

namespace _123TribeFrameworker.Controllers
{
    [AllowAnonymous]
    public class OutRecordsController : Controller
    {
        [Dependency]
        public IOutRecordsLayer layer { get; set; }
        public OutRecordsController()
        {
        }
        // GET: OutRecords
        public ActionResult Index()
        {
            var result = layer.getRecordsCount();
            return View();
        }
    }
}