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

                using (OmgevingsboekContext context = new OmgevingsboekContext())
                {
                    GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);
                    GebruikerRepository gebruikersrepo=new GebruikerRepository(context);

                    currentUser=gebruikersrepo.GetByEmail(user);


                    ViewBag.gebruiker = currentUser;

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