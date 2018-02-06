using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _123TribeFrameworker.Controllers
{
    public class DirectoryController : Controller
    {
        // GET: Directory
        public ActionResult Index()
        {
            return View("_Layout");
        }
    }
}