using DigitaalOmgevingsboek.Businesslayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitaalOmgevingsboek.Controllers
{
    public class POIController : Controller
    {
        private IPOIService ps;

        public POIController(IPOIService ps)
        {
            this.ps = ps;
        }

        // GET: POI
        public ActionResult POIStart()
        {
            return View();
        }

        public ActionResult POIOverzicht()
        {
            List<POI> pois = ps.GetPOIs();
            return View(pois);
        }

        public ActionResult POINewModify()
        {
            return View();
        }


        public ActionResult POIActivity()
        {
            return View();
        }


        public ActionResult POIView()
        {
            return View();
        }
    }
}