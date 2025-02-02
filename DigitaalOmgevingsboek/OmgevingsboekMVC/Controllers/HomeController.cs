﻿using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using OmgevingsboekMVC.Businesslayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmgevingsboekMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {

                string user = User.Identity.Name;
                AspNetUsers currentUser = new AspNetUsers();
                List<POI> allPOI = new List<POI>();
                List<Uitstap> allUistappen = new List<Uitstap>();
                List<AspNetUsers> allGebruikers = new List<AspNetUsers>();
               

                using (OmgevingsboekContext context = new OmgevingsboekContext())
                {
                    GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>();
                    POIRepository POIrepo = new POIRepository(context);
                    GenericRepository<POI_Log> logrepo = new GenericRepository<POI_Log>();
                    UitstapRepository Uitstaprep = new UitstapRepository(context);
                    GebruikerRepository gebruikersrepo=new GebruikerRepository(context);

                    currentUser=gebruikersrepo.GetByEmail(user);
                    List<POI_Log> logLijst = new List<POI_Log>();
                    logLijst = logrepo.All().ToList();

                   
                   
                    

                    foreach (POI poi in POIrepo.All().ToList())
                    {
                        if(poi.IsDeleted==false)
                        {
                            allPOI.Add(poi);
                        }
                    }

                    foreach(Uitstap uitstap in Uitstaprep.All().ToList())
                    {
                        if(uitstap.IsDeleted==false)
                        {
                            allUistappen.Add(uitstap);
                        }
                    }

                    foreach(AspNetUsers aspnetuser in gebruikersrepo.All().ToList() )
                    {
                        if((aspnetuser.IsDeleted==false) && (aspnetuser.IsPending==false) && (aspnetuser.EmailConfirmed==true))
                        {
                            allGebruikers.Add(aspnetuser);

                        }
                    }


                    logLijst.Reverse();
                    

                    ViewBag.gebruiker = currentUser;
                    ViewBag.lijstPOI = allPOI;
                    ViewBag.lijstUitstappen = allUistappen;
                    ViewBag.lijstGebruikers = allGebruikers;
                    ViewBag.lijstLogs = logLijst;


                }
                
                return View(currentUser);}
            return RedirectToAction("Login", "Account");
          
        }

        public ActionResult GebruikersProfiel()
        {
            AspNetUsers currentUser = new AspNetUsers();
            string user = User.Identity.Name;
            using (OmgevingsboekContext context = new OmgevingsboekContext())
            {
                GebruikerRepository gebruikersrepo = new GebruikerRepository(context);

                currentUser = gebruikersrepo.GetByEmail(user);
 
            }

            return View(currentUser);
        }

        [HttpPost]
        public ActionResult GebruikersProfiel(AspNetUsers user)
        {
            using (OmgevingsboekContext context = new OmgevingsboekContext())
            {
                GebruikerRepository gebruikersrepo = new GebruikerRepository(context);

                gebruikersrepo.Update(user);
                gebruikersrepo.SaveChanges();

            }
            return View("GebruikersProfiel");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Manual()
        {
            return View();
        }
    }
}