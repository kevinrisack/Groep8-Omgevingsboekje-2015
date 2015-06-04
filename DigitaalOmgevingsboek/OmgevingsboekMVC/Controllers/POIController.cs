using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace DigitaalOmgevingsboek.Controllers
{
    public class POIController : Controller
    {
        private IPOIService ps;

        public POIController(IPOIService ps)
        {
            this.ps = ps;
        }

        public ActionResult POIStart()
        {
            return View();
        }

        public ActionResult POIOverzicht()
        {
            List<POI> pois = ps.GetPOIs();
            ViewBag.Doelgroepen = ps.GetDoelgroepen();
            ViewBag.Leerdoelen = ps.GetLeerdoelen();
            return View(pois);
        }

        [HttpGet]
        public ActionResult POINewModify(int? id)
        {
            POI poi = new POI();
            if (id.HasValue)
            {
                poi = ps.GetPOI(id.Value);
            }
            else
            {
                poi.Auteur_Id = User.Identity.GetUserId();
            }
            
            ViewBag.Doelgroepen = ps.GetDoelgroepen();

            return View(poi);
        }

        [HttpPost]
        public ActionResult POINewModify(POI poi, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ps.AddOrUpdatePOI(poi);
                    if (picture != null)
                    {
                        ps.UploadPicture(poi, picture);
                    }
                    return RedirectToAction("POIOverzicht");
                }
                catch (Exception e)
                {
                    return View("Error: " + e);
                }
            }
            else
            {
                return RedirectToAction("POINewModify", poi);
            }
        }


        public ActionResult POIActivity()
        {
            return View();
        }


        public ActionResult POIView(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("POIOverzicht");
            }

            POI poi = ps.GetPOI(id.Value);

            return View(poi);
        }
    }
}