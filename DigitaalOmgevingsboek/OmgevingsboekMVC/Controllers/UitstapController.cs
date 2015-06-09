using DigitaalOmgevingsboek;
using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace OmgevingsboekMVC.Controllers
{
    [Authorize]
    public class UitstapController : Controller
    {
        UitstapService us;

        public UitstapController(UitstapService us)
        {
            this.us = us;
        }
        
        // GET: Uitstap
        public ActionResult Index(string filter)
        {
            List<Uitstap> uitstappen;
            
            if (filter == null)
            {
                if (User.IsInRole("Administrator"))
                {
                    uitstappen = us.GetUitstappen();
                    return View(uitstappen);
                }
                else
                    return RedirectToAction("Index", "Uitstap", new { filter = "my" });
            }
            
            switch(filter)
            {
                case "my":  uitstappen = us.GetUitstappen(User.Identity.GetUserId());
                            return View(uitstappen);

                case "all": uitstappen = us.GetUitstappen();
                            List<Uitstap> uitstappenMetRechten = new List<Uitstap>();
                            foreach (Uitstap u in uitstappen)
                            {
                                foreach (AspNetUsers user in u.AspNetUsers1)
                                {
                                    if (user.Id == User.Identity.GetUserId())
                                        uitstappenMetRechten.Add(u);
                                }
                            }
                            List<Uitstap> uitstappenEigen = us.GetUitstappen(User.Identity.GetUserId());
                            foreach (Uitstap u in uitstappenEigen)
                                uitstappenMetRechten.Add(u);

                            return View(uitstappenMetRechten);

                default: return RedirectToAction("Index", "Uitstap", new { filter = "my"});
            }
        }

        public ActionResult New()
        {
            Uitstap uitstap = new Uitstap();
            return View(uitstap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Uitstap uitstap)
        {
            if (!ModelState.IsValid)
                return View(uitstap);

            uitstap.IsDeleted = false;
            uitstap.Auteur_Id = User.Identity.GetUserId();
            uitstap.Route = new Route();
            uitstap.POI = new List<POI>();
            uitstap.AspNetUsers1 = new List<AspNetUsers>();

            uitstap = us.AddUitstap(uitstap);

            return RedirectToAction("Edit", new { id = uitstap.Id});
        }

        

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Uitstap uitstap = us.GetUitstap(id.Value);

            if (uitstap.Naam != null)
            {
                ViewBag.POI = us.GetPOIs();
                return View(uitstap);
            }
            else
                return RedirectToAction("New");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Uitstap uitstap, string submit)
        {
            Uitstap originalUitstap = us.GetUitstap(uitstap.Id);

            if (originalUitstap.Naam != uitstap.Naam) originalUitstap.Naam = uitstap.Naam;
            if (originalUitstap.Beschrijving != uitstap.Beschrijving) originalUitstap.Beschrijving = uitstap.Beschrijving;
            
            string[] input = submit.Split(':');

            switch (input[0])
            {
                case "save":
                    if (!ModelState.IsValid)
                        return View(uitstap);

                    return RedirectToAction("Details", uitstap.Id);

                case "delete":
                    originalUitstap.POI.Remove(us.GetPOIById(int.Parse(input[1])));
                    us.UpdateUitstap(originalUitstap);
                    return RedirectToAction("Edit", new { id = originalUitstap.Id });

                case "add":
                    originalUitstap.POI.Add(us.GetPOIById(int.Parse(input[1])));
                    us.UpdateUitstap(originalUitstap);
                    return RedirectToAction("Edit", new { id = originalUitstap.Id });

                default: return RedirectToAction("Index", new { filter = "my" });

            }
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            
            Uitstap uitstap = us.GetUitstap(id.Value);
            if (!uitstap.IsDeleted)
                return View(uitstap);
            else
                return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            
            Uitstap uitstap = us.GetUitstap(id.Value);
            return View(uitstap);
        }
    }
}