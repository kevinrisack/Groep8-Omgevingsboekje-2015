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
      [Authorize(Roles="Administrator")]
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

        public ActionResult Toestaan(AspNetUsers aspuser)
        {
            using(OmgevingsboekContext context=new OmgevingsboekContext())
            {
                GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);
                aspuser.IsPending = false;
                repo.Update(aspuser);
                repo.SaveChanges();

            }



            return RedirectToAction("Index");
        }
        public ActionResult Weigeren(AspNetUsers aspuser)
        {
            using (OmgevingsboekContext context = new OmgevingsboekContext())
            {
                GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);

                repo.Delete(aspuser);
                repo.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        public ActionResult Verwijderen(AspNetUsers aspuser)
        {
            using (OmgevingsboekContext context = new OmgevingsboekContext())
            {
                GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);
                aspuser.IsDeleted = true;

                repo.Update(aspuser);
                repo.SaveChanges();


            }

            return RedirectToAction("Index");
        }

        public ActionResult Activeren(AspNetUsers aspuser)
        {
            using (OmgevingsboekContext context = new OmgevingsboekContext())
            {
                GenericRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>(context);
                aspuser.IsDeleted = false;

                repo.Update(aspuser);
                repo.SaveChanges();


            }

            return RedirectToAction("Index");
        }
        
    }
}