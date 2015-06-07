using DigitaalOmgevingsboek;
using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OmgevingsboekMVC.Attributes;

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
            uitstap.Auteur_Id = User.Identity.GetUserId();
            ViewBag.POIs = us.GetPOIs();
            return View(uitstap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Uitstap uitstap, string submit)
        {
            ViewBag.POIs = us.GetPOIs();
            string[] input = submit.Split(':');

            switch (input[0])
            {
                case "save":
                    if (!ModelState.IsValid)
                        return View(uitstap);

                    us.UpdateUitstap(uitstap);
                    return RedirectToAction("Details", uitstap.Id);

                case "delete":
                    uitstap.POI.Remove(us.GetPOIById(int.Parse(input[1])));
                    return View(uitstap);

                case "add":
                    uitstap.POI.Add(us.GetPOIById(int.Parse(input[1])));
                    return View(uitstap);

                default: return RedirectToAction("Index", "my");

            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Uitstap uitstap = us.GetUitstap(id.Value);

            if (uitstap.Naam != null)
                return View(uitstap);
            else
                return RedirectToAction("New");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Uitstap uitstap)
        {
            if (ModelState.IsValid)
            {
                us.UpdateUitstap(uitstap);
                return RedirectToAction("Details", uitstap.Id);
            }
            else
                return View(uitstap);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");
            
            Uitstap uitstap = us.GetUitstap(id.Value);
            return View(uitstap);
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