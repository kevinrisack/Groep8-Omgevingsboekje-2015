using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DigitaalOmgevingsboek.Businesslayer.Services;

namespace DigitaalOmgevingsboek.Controllers
{
    [Authorize]
    public class POIController : Controller
    {
        private POIService ps;
        private OmgevingsboekContext db = new OmgevingsboekContext();

        public POIController(POIService ps)
        {
            this.ps = ps;
        }

        #region POI
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

            ViewBag.UserId = User.Identity.GetUserId();

            return View(pois);
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

        [HttpGet]
        public ActionResult POINew()
        {
            POI poi = new POI();
            poi.Auteur_Id = User.Identity.GetUserId();

            ViewBag.Doelgroepen = ps.GetDoelgroepen();
            ViewBag.Themas = ps.GetThemas();

            return View(poi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult POINew(POI poi, HttpPostedFileBase picture, List<int> doelgroepIds, List<int> themaIds)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    ps.AddPOI(poi);

                    if (doelgroepIds != null)
                    {
                        foreach (int doelgroepId in doelgroepIds)
                        {
                            Doelgroep dg = ps.GetDoelgroep(doelgroepId);

                            dg.POI.Add(poi);
                            poi.Doelgroep.Add(dg);

                            ps.UpdateDoelgroep(dg);
                        }
                    }

                    if (themaIds != null)
                    {
                        foreach (int themaId in themaIds)
                        {
                            Thema th = ps.GetThema(themaId);

                            th.POI.Add(poi);
                            poi.Thema.Add(th);

                            ps.UpdateThema(th);
                        }
                    }
                    
                    ps.UpdatePOI(poi);

                    if (picture != null)
                    {
                        ps.UploadPicturePOI(poi, picture);
                    }
                    return RedirectToAction("POIView", new { id = poi.Id });
                }
                catch (Exception e)
                {
                    return View("Error: " + e);
                }
            }
            else
            {
                if (doelgroepIds != null)
                {
                    foreach (int doelgroepId in doelgroepIds)
                    {
                        Doelgroep dg = ps.GetDoelgroep(doelgroepId);
                        poi.Doelgroep.Add(dg);
                    }
                }
                if (themaIds != null)
                {
                    foreach (int themaId in themaIds)
                    {
                        Thema th = ps.GetThema(themaId);
                        poi.Thema.Add(th);
                    }
                }
                ViewBag.Doelgroepen = ps.GetDoelgroepen();
                ViewBag.Themas = ps.GetThemas();
                return View(poi);
            }
        }

        [HttpGet]
        public ActionResult POIModify(int? id)
        {
            if (id.HasValue)
            {
                POI poi = ps.GetPOI(id.Value);

                ViewBag.Doelgroepen = ps.GetDoelgroepen();
                ViewBag.Themas = ps.GetThemas();

                return View(poi);
            }
            else
            {
                return RedirectToAction("POIOverzicht");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult POIModify(POI poi, HttpPostedFileBase picture, List<int> doelgroepIds, List<int> themaIds)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    ps.UpdatePOI(poi);
                    poi = ps.GetPOI(poi.Id);

                    poi.Doelgroep = new List<Doelgroep>();
                    if (doelgroepIds != null)
                    {
                        foreach (int doelgroepId in doelgroepIds)
                        {
                            Doelgroep dg = ps.GetDoelgroep(doelgroepId);

                            dg.POI.Add(poi);
                            poi.Doelgroep.Add(dg);

                            ps.UpdateDoelgroep(dg);
                        }
                    }

                    poi.Thema = new List<Thema>();
                    if (themaIds != null)
                    {
                        foreach (int themaId in themaIds)
                        {
                            Thema th = ps.GetThema(themaId);

                            th.POI.Add(poi);
                            poi.Thema.Add(th);

                            ps.UpdateThema(th);
                        }
                    }

                    ps.UpdatePOI(poi);

                    if (picture != null)
                    {
                        ps.UploadPicturePOI(poi, picture);
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
                ViewBag.Doelgroepen = ps.GetDoelgroepen();
                ViewBag.Themas = ps.GetThemas();
                return View(poi);
            }
        }

        public ActionResult POIDelete(int? id)
        {
            if (id.HasValue)
            {
                POI poi = ps.GetPOI(id.Value);

                if (poi.Auteur_Id == User.Identity.GetUserId())
                {
                    poi.IsDeleted = true;
                    ps.UpdatePOI(poi);
                    return RedirectToAction("POIOverzicht");
                }
                else
                {
                    return View("Error: " + "U heeft geen toestemming op deze POI te verwijderen.");
                }     
            }
            return RedirectToAction("POIOverzicht");
        }
        #endregion

        #region Activiteit
        public ActionResult ActivityView(int? id)
        {
            if (id.HasValue)
            {
                Activiteit activiteit = ps.GetActiviteit(id.Value);
                return View(activiteit);
            }
            else
            {
                return RedirectToAction("POIStart");
            }
        }

        [HttpGet]
        public ActionResult ActivityNew(int? id)
        {
            if (id.HasValue)
            {
                Activiteit activiteit = new Activiteit();
                activiteit.POI_Id = id.Value;

                ViewBag.Doelgroepen = ps.GetDoelgroepen();
                ViewBag.Leerdoelen = ps.GetLeerdoelen();

                return View(activiteit);
            }
            else
            {
                return RedirectToAction("POIOverzicht");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivityNew(Activiteit activiteit, HttpPostedFileBase picture, List<int> doelgroepIds, List<int> leerdoelIds)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    ps.AddActiviteit(activiteit);

                    if (doelgroepIds != null)
                    {
                        foreach (int doelgroepId in doelgroepIds)
                        {
                            Doelgroep dg = ps.GetDoelgroep(doelgroepId);

                            dg.Activiteit.Add(activiteit);
                            activiteit.Doelgroep.Add(dg);

                            ps.UpdateDoelgroep(dg);
                        }
                    }

                    if (leerdoelIds != null)
                    {
                        foreach (int leerdoelId in leerdoelIds)
                        {
                            Leerdoel ld = ps.GetLeerdoel(leerdoelId);

                            ld.Activiteit.Add(activiteit);
                            activiteit.Leerdoel.Add(ld);

                            ps.UpdateLeerdoel(ld);
                        }
                    }
                    
                    ps.UpdateActiviteit(activiteit);

                    if (picture != null)
                    {
                        ps.UploadPictureActiviteit(activiteit, picture);
                    }
                    return RedirectToAction("ActivityView", new { id = activiteit.Id });
                }
                catch (Exception e)
                {
                    return View("Error: " + e);
                }
            }
            else
            {
                if (doelgroepIds != null)
                {
                    foreach (int doelgroepId in doelgroepIds)
                    {
                        Doelgroep dg = ps.GetDoelgroep(doelgroepId);
                        activiteit.Doelgroep.Add(dg);
                    }
                }
                if (leerdoelIds != null)
                {
                    foreach (int leerdoelId in leerdoelIds)
                    {
                        Leerdoel ld = ps.GetLeerdoel(leerdoelId);
                        activiteit.Leerdoel.Add(ld);
                    }
                }
                ViewBag.Doelgroepen = ps.GetDoelgroepen();
                ViewBag.Leerdoelen = ps.GetLeerdoelen();
                return View(activiteit);
            }
        }
        #endregion
    }
}