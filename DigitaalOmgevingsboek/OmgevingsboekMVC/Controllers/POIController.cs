using OmgevingsboekMVC.Businesslayer.Services;
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

        public ActionResult Details(int? poiId)
        {
            if (!poiId.HasValue)
            {
                return RedirectToAction("Index");
            }

            POI poi = ps.GetPOI(poiId.Value);

            return View(poi);
        }

        [HttpGet]
        public ActionResult New()
        {
            POI poi = new POI();
            //poi.Auteur_Id = User.Identity.GetUserId();

            return View(poi);
        }

        [HttpPost]
        public ActionResult New(POI poi)
        {
            try
            {
                poi.Auteur_Id = "da65898a-accc-4d09-b1ef-2ebcdbd35eb8";
                //if (ModelState.IsValid)
                //{
                ps.AddPOI(poi);
                //}
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error: " + e);
            }
        }
    }
}