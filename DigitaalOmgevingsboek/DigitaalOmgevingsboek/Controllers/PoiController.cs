using DigitaalOmgevingsboek.BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitaalOmgevingsboek.Controllers
{
    public class PoiController : Controller
    {
        private IPoiService ps;

        public PoiController(IPoiService ps)
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