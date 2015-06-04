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
            ViewBag.Doelgroepen = ps.GetDoelgroepen();
            ViewBag.Themas = ps.GetThemas();
            return View();
        }

        public ActionResult POIOverzicht(int? themaId, int? doelgroepId)
        {
            List<POI> pois = new List<POI>();

            //get POI by thema
            if (themaId.HasValue) 
            {
                pois = ps.GetPOIByThema(themaId.Value);
            }
            //get POI by doelgroep
            else if (doelgroepId.HasValue)
            {
                pois = ps.GetPOIByDoelgroep(doelgroepId.Value);
            }
            //get all POIs
            else
            {
                pois = ps.GetPOIs();
            }
            
            ViewBag.Doelgroepen = ps.GetDoelgroepen();
            ViewBag.Leerdoelen = ps.GetLeerdoelen();
            return View(pois);
        }

        [HttpGet]
        public ActionResult POIModify(int? id)
        {
            if (id.HasValue)
            {
                POI poi = ps.GetPOI(id.Value);

                ViewBag.Doelgroepen = ps.GetDoelgroepen();

                return View(poi);
            }
            else
            {
                return RedirectToAction("POIOverzicht");
            }
        }

        [HttpPost]
        public ActionResult POIModify(POI poi, HttpPostedFileBase picture, List<int> doelgroepIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //if (doelgroepIds != null)
                    //{
                    //    foreach (int doelgroepId in doelgroepIds)
                    //    {
                    //        Doelgroep dg = ps.GetDoelgroep(doelgroepId);
                    //        //dg.POI.Add(poi);
                    //        poi.Doelgroep.Add(dg);
                    //        //ps.UpdateDoelgroep(dg);
                    //    }
                    //}

                    
                    ps.UpdatePOI(poi);

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
                return RedirectToAction("POIModify", poi);
            }
        }

        [HttpGet]
        public ActionResult POINew(POI poi)
        {
            POI poiNew = new POI();
            if (poi != null)
            {
                poiNew = poi;
            }

            poiNew.Auteur_Id = User.Identity.GetUserId();

            ViewBag.Doelgroepen = ps.GetDoelgroepen();

            return View(poiNew);
        }

        [HttpPost]
        public ActionResult POINew(POI poi, HttpPostedFileBase picture, List<int> doelgroepIds)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ps.AddPOI(poi);
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
                return RedirectToAction("POINew", poi);
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