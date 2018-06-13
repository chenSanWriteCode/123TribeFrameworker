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
    public class InStorageRecordController : Controller
    {
        
        // GET: InStorageRecord
        public ActionResult Index()
        {
            return View();
        }
       
        
    }
}