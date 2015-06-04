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
            
            if (filter == "")
            {
                uitstappen = us.GetUitstappen();
                return View(uitstappen);
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
                            return View(uitstappenMetRechten);

                default: return RedirectToAction("Index", "my");
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