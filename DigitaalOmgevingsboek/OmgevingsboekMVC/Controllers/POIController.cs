using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
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
        private GenericRepository<Doelgroep> psDoelgroep;
        private GenericRepository<Leerdoel> psLeerdoel;

        public POIController(IPOIService ps, GenericRepository<Doelgroep> psDoelgroep, GenericRepository<Leerdoel> psLeerdoel)
        {
            this.ps = ps;
            this.psDoelgroep = psDoelgroep;
            this.psLeerdoel = psLeerdoel;
        }

        // GET: POI
        public ActionResult POIStart()
        {
            return View();
        }

        public ActionResult POIOverzicht()
        {
            List<POI> pois = ps.GetPOIs();
            ViewBag.Doelgroepen = psDoelgroep.All();
            ViewBag.Leerdoelen = psLeerdoel.All();
            return View(pois);
        }

        [HttpGet]
        public ActionResult POINewModify()
        {
            POI poi = new POI();
            IEnumerable<Doelgroep> doelgroepen = psDoelgroep.All();
            //poi.Auteur_Id = User.Identity.GetUserId();

            ViewBag.Doelgroepen = doelgroepen;

            return View(poi);
        }

        [HttpPost]
        public ActionResult POINewModify(POI poi)
        {
            if (ModelState.IsValid)
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