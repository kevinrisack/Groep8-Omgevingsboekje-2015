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

        [AllowAnonymous]
        public ActionResult ViewPdf(int? id)
        {
            try
            {
                POI poi = ps.GetPOI(id.Value);

                return new Rotativa.ViewAsPdf("POIPdfView", poi) { FileName = poi.Naam + ".pdf" };
            }
            catch (Exception)
            {

                return RedirectToAction("POIStart");
            }
        }

        public ActionResult POIPdfView(POI poiPDF)
        {
            return View(poiPDF);
        }
        public ActionResult POIOverzicht(string leergebiedNaam, int? doelgroepId, string filter)
        {
            List<POI> pois = new List<POI>();

            //get POI by thema
            if (leergebiedNaam!="" && leergebiedNaam!=null) 
            {
                pois = ps.GetPOIByThema(leergebiedNaam);
                ViewBag.Filter = leergebiedNaam;
            }
            //get POI by doelgroep
            else if (doelgroepId.HasValue)
            {
                pois = ps.GetPOIByDoelgroep(doelgroepId.Value);
                ViewBag.Filter = ps.GetDoelgroep(doelgroepId.Value).DoelgroepNaam;
            }
            //get POI by user
            else if (filter != "" && filter != null)
            {
                switch (filter)
                {
                    //get all POIs
                    case "alle":
                        pois = ps.GetPOIs();
                        ViewBag.Filter = "Alle POI's";
                        break;
                    //get POI by user
                    case "mijn":
                        pois = ps.GetPOIByUser(User.Identity.GetUserId());
                        ViewBag.Filter = "Mijn POI's";
                        break;
                }
            }
            else
            {
                pois = ps.GetPOIs();
                ViewBag.Filter = "Alle POI's";
            }
            
            ViewBag.Doelgroepen = ps.GetDoelgroepen();
            

            ViewBag.UserId = User.Identity.GetUserId();

            return View(pois);
        }

        [HttpGet]
        public ActionResult POIView(int? id)
        {           
            try
            {   
                POI poi;
                poi = ps.GetPOI(id.Value);
                ViewBag.UserId = User.Identity.GetUserId();
                return View(poi);
            }
            catch (Exception)
            {
                return RedirectToAction("POIStart");
            }
        }

        [HttpPost]
        public ActionResult POIView(int? id, string reactie)
        {            
            try
            {
                POI poi;
                poi = ps.GetPOI(id.Value);
                return View(poi);
            }
            catch (Exception)
            {

                return RedirectToAction("POIStart");
            }        
        }

        [HttpGet]
        public ActionResult POINew()
        {
            POI poi = new POI();
            poi.Auteur_Id = User.Identity.GetUserId();

            ViewBag.Doelgroepen = ps.GetDoelgroepen();
            ViewBag.Leergebieden = ps.GetThemas();

            return View(poi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult POINew(POI poi, List<HttpPostedFileBase> pictures, List<int> doelgroepIds, List<int> themaIds)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    ps.AddPOI(poi);
                    using (OmgevingsboekContext context = new OmgevingsboekContext())
                    {
                        GenericRepository<POI_Log> repo = new GenericRepository<POI_Log>();
                        POI_Log log = new POI_Log();
                        log.Event = "Nieuwe POI aangemaakt";
                        log.POI_Id = poi.Id;
                        log.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        repo.Insert(log);
                        repo.SaveChanges();
                    }

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

                    if (!pictures.Contains(null))
                    {
                        foreach (HttpPostedFileBase picture in pictures)
                        {
                            ps.UploadPicturePOI(poi, picture);
                        }                       
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
                ViewBag.Leergebieden = ps.GetThemas();
                return View(poi);
            }
        }

        [HttpGet]
        public ActionResult POIModify(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    POI poi = ps.GetPOI(id.Value);

                    if (poi.Auteur_Id == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                    {
                        ViewBag.Doelgroepen = ps.GetDoelgroepen();
                        ViewBag.Leergebieden = ps.GetThemas();

                        return View(poi); 
                    }
                    else
                    {
                        return RedirectToAction("POIView", new { id = poi.Id });
                    }
                }
                else
                {
                    return RedirectToAction("POIStart");
                }
            }
            catch (Exception)
            {      
                return RedirectToAction("POIStart");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult POIModify(POI poi, List<HttpPostedFileBase> pictures, List<int> doelgroepIds, List<int> LeergebiedenIds, List<string> deleteFotoURLs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ps.UpdatePOI(poi);
                    using (OmgevingsboekContext context = new OmgevingsboekContext())
                    {
                        GenericRepository<POI_Log> repo = new GenericRepository<POI_Log>();
                        POI_Log log = new POI_Log();
                        log.Event = "POI gewijzigd";
                        log.POI_Id = poi.Id;
                        log.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        repo.Insert(log);
                        repo.SaveChanges();
                    }
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
                    if (LeergebiedenIds != null)
                    {
                        foreach (int LeergebiedId in LeergebiedenIds)
                        {
                            Thema th = ps.GetThema(LeergebiedId);

                            th.POI.Add(poi);
                            poi.Thema.Add(th);

                            ps.UpdateThema(th);
                        }
                    }

                    if (deleteFotoURLs != null)
                    {
                        foreach (string deleteFotoURL in deleteFotoURLs)
                        {
                            if(poi.Foto_POI.Any(fp => fp.FotoURL == deleteFotoURL))
                            {
                                Foto_POI fotoPOI = poi.Foto_POI.First(fp => fp.FotoURL == deleteFotoURL);
                                ps.DeleteFotoPOI(poi, fotoPOI);
                            }
                        }
                    }

                    ps.UpdatePOI(poi);

                    if (!pictures.Contains(null))
                    {
                        foreach (HttpPostedFileBase picture in pictures)
                        {
                            ps.UploadPicturePOI(poi, picture);
                        }
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
                poi = ps.GetPOI(poi.Id);
                ViewBag.Doelgroepen = ps.GetDoelgroepen();
                ViewBag.Leergebieden = ps.GetThemas();
                return View(poi);
            }
        }

        public ActionResult POIDelete(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    POI poi = ps.GetPOI(id.Value);

                    if (poi.Auteur_Id == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                    {
                        poi.IsDeleted = true;
                        ps.UpdatePOI(poi);
                        using (OmgevingsboekContext context = new OmgevingsboekContext())
                        {
                            GenericRepository<POI_Log> repo = new GenericRepository<POI_Log>();
                            POI_Log log = new POI_Log();
                            log.Event = "POI verwijderd";
                            log.POI_Id = poi.Id;
                            log.Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            repo.Insert(log);
                            repo.SaveChanges();
                        }
                        return RedirectToAction("POIStart");
                    }
                    else
                    {
                        return RedirectToAction("POIView", new { id = poi.Id });
                    }
                }
                return RedirectToAction("POIStart");
            }
            catch (Exception)
            {
                
                return RedirectToAction("POIStart");
            }
        }
        #endregion

        #region Activiteit
        public ActionResult ActivityView(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    Activiteit activiteit = ps.GetActiviteit(id.Value);
                    ViewBag.UserId = User.Identity.GetUserId();
                    return View(activiteit);
                }
                else
                {
                    return RedirectToAction("POIStart");
                }
            }
            catch (Exception)
            {
                
                return RedirectToAction("POIStart");
            }
        }

        [HttpGet]
        public ActionResult ActivityNew(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    Activiteit activiteit = new Activiteit();
                    activiteit.POI_Id = id.Value;

                    ViewBag.Doelgroepen = ps.GetDoelgroepen();


                    return View(activiteit);
                }
                else
                {
                    return RedirectToAction("POIStart");
                }
            }
            catch (Exception)
            {
                
                return RedirectToAction("POIStart");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivityNew(Activiteit activiteit, List<HttpPostedFileBase> pictures, List<int> doelgroepIds)
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

                   
                    
                    ps.UpdateActiviteit(activiteit);

                    if (!pictures.Contains(null))
                    {
                        foreach (HttpPostedFileBase picture in pictures)
                        {
                            ps.UploadPictureActiviteit(activiteit, picture);
                        }
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
                
                ViewBag.Doelgroepen = ps.GetDoelgroepen();
             
                return View(activiteit);
            }
        }

        [HttpGet]
        public ActionResult ActivityModify(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    Activiteit activiteit = ps.GetActiviteit(id.Value);

                    if (activiteit.POI.Auteur_Id == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                    {
                        ViewBag.Doelgroepen = ps.GetDoelgroepen();

                        return View(activiteit);
                    }
                    else
                    {
                        return RedirectToAction("ActivityView", new { id = activiteit.Id });
                    }
                }
                else
                {
                    return RedirectToAction("POIStart");
                }
            }
            catch (Exception)
            {
                
                return RedirectToAction("POIStart");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActivityModify(Activiteit activiteit, List<HttpPostedFileBase> pictures, List<int> doelgroepIds,List<string> deleteFotoURLs)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    ps.UpdateActiviteit(activiteit);
                    activiteit = ps.GetActiviteit(activiteit.Id);

                    activiteit.Doelgroep = new List<Doelgroep>();
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

                   

                    if (deleteFotoURLs != null)
                    {
                        foreach (string deleteFotoURL in deleteFotoURLs)
                        {
                            if (activiteit.Foto_Activiteit.Any(fa => fa.URL == deleteFotoURL))
                            {
                                Foto_Activiteit fotoActiviteit = activiteit.Foto_Activiteit.First(fa => fa.URL == deleteFotoURL);
                                ps.DeleteFotoActiviteit(activiteit, fotoActiviteit);
                            }
                        }
                    }

                    ps.UpdateActiviteit(activiteit);

                    if (!pictures.Contains(null))
                    {
                        foreach (HttpPostedFileBase picture in pictures)
                        {
                            ps.UploadPictureActiviteit(activiteit, picture);
                        }
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
                ViewBag.Doelgroepen = ps.GetDoelgroepen();
               
                return View(activiteit);
            }
        }

        public ActionResult ActivityDelete(int? id)
        {
            try
            {
                Activiteit act = ps.GetActiviteit(id.Value);

                if (act.POI.Auteur_Id == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                {
                    int poiId = act.POI.Id;
                    ps.DeleteActiviteit(act);
                    return RedirectToAction("POIView", new { id = poiId });
                }
                else
                {
                    return RedirectToAction("ActivityView", new { id = act.Id });
                }
            }
            catch(Exception)
            {
                return RedirectToAction("POIStart");
            }
        }
        #endregion
    }
}