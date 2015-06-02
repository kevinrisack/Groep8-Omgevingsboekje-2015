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
        public ActionResult Index()
        {
            List<POI> pois = ps.GetPOIs();
            return View(pois);
        }
    }
}