using DigitaalOmgevingsboek;
using OmgevingsboekMVC.Businesslayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmgevingsboekMVC.Controllers
{
    public class GebruikerController : Controller
    {
        // GET: Gebruiker
        public ActionResult Index()
        { 
            List<AspNetUsers> lijst = new List<AspNetUsers>();
            using(OmgevingsboekContext context =new OmgevingsboekContext())
            {
               
                GebruikerRepository gebruikerrepo=new GebruikerRepository(context);
                lijst = gebruikerrepo.All().ToList();
            }
            ViewBag.Gebruikers = lijst;
            return View("GebruikersOverzicht");
        }
    }
}