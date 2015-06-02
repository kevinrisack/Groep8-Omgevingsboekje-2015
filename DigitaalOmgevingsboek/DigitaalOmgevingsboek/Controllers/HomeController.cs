using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapSite1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title="Testcase";
return View();
        }

        public ActionResult Home()
        {
            ViewBag.Title = "Testcase";
            return View();
        }

        public ActionResult Uitstappen()
        {
            ViewBag.Title = "Testcase";
            return View();
        }
    }
}