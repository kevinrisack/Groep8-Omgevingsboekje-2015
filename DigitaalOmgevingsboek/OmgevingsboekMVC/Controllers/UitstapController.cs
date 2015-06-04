using DigitaalOmgevingsboek;
using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Uitstap uitstap = us.GetUitstap(id.Value);
            return View(uitstap);
        }

        [HttpPost]
        public ActionResult Edit(Uitstap uitstap)
        {
            if (ModelState.IsValid)
            {
                us.AddUitstap(uitstap);
                return RedirectToAction("Details", uitstap.Id);
            }
            else
                return RedirectToAction("Edit", uitstap.Id);
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