using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using OmgevingsboekMVC.Businesslayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmgevingsboekMVC.Controllers
{
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
                    UitstapRepository Uitstaprep = new UitstapRepository(context);
                    GebruikerRepository gebruikersrepo=new GebruikerRepository(context);

                    currentUser=gebruikersrepo.GetByEmail(user);
                    
                   
                    

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

                    ViewBag.gebruiker = currentUser;
                    ViewBag.lijstPOI = allPOI;
                    ViewBag.lijstUitstappen = allUistappen;
                    ViewBag.lijstGebruikers = allGebruikers;


                }
                
                return View(currentUser);}
            return RedirectToAction("Login", "Account");
          
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
    }
}