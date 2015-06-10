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
            List<Uitstap> uitstappen = us.GetUitstappen();

            if (filter == null)
                return RedirectToAction("Index", "Uitstap", new { filter = "all" });

            List<Uitstap> uitstappenMine = us.GetUitstappen(User.Identity.GetUserId());

            List<Uitstap> uitstappenMetRechten = new List<Uitstap>();
            foreach (Uitstap u in uitstappen)
            {
                foreach (AspNetUsers user in u.AspNetUsers1)
                {
                    if (user.Id == User.Identity.GetUserId())
                        uitstappenMetRechten.Add(u);
                }
            }

            foreach (Uitstap u in uitstappenMine)
                uitstappenMetRechten.Add(u);

            ViewBag.nAll = uitstappenMetRechten.Count;
            ViewBag.nMine = us.GetUitstappen(User.Identity.GetUserId()).Count;
            
            switch(filter)
            {
                case "my": 
                            return View(uitstappenMine);

                case "all": 
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

            foreach (AspNetUsers anu in us.GetUsers())
                foreach (AspNetRoles role in anu.AspNetRoles)
                    if (role.Name.Equals("Administrator"))
                        uitstap.AspNetUsers1.Add(anu);

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
                ViewBag.Users = us.GetUsers();

                List<POI> poiInRoute = new List<POI>();
                try
                {
                    foreach (string s in uitstap.Route.Points.Split(';').ToList<string>())
                        poiInRoute.Add(us.GetPOIById(int.Parse(s)));
                }
                catch (Exception) { }

                ViewBag.Points = poiInRoute;

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
            ViewBag.POI = us.GetPOIs();
            ViewBag.Users = us.GetUsers();

            ViewBag.Points = GetPoisInRoute(originalUitstap);

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
                    switch (input[1])
                    {
                        case "route":
                            List<POI> poisInRoute = ViewBag.Points;
                            poisInRoute.Remove(us.GetPOIById(int.Parse(input[2])));

                            ViewBag.Points = poisInRoute;

                            string newPoints = null;
                            if (poisInRoute.Count != 0)
                            {
                                newPoints = poisInRoute.First().Id.ToString(); poisInRoute.RemoveAt(0);
                                foreach (POI point in poisInRoute)
                                    newPoints += ";" + point.Id; 
                            }
                            else
                                newPoints = null;

                            originalUitstap.Route.Points = newPoints;

                            us.UpdateUitstap(originalUitstap);
                                    
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });
                        
                        case "poi":
                            originalUitstap.POI.Remove(us.GetPOIById(int.Parse(input[2])));
                            us.UpdateUitstap(originalUitstap);
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });

                        case "user":
                            originalUitstap.AspNetUsers1.Remove(us.GetUserById(input[2]));
                            us.UpdateUitstap(originalUitstap);
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });

                        default: return RedirectToAction("Index", new { filter = "my" });
                    }


                case "add":
                    switch(input[1])
                    {
                        case "route":
                            if (originalUitstap.Route.Points != null)
                                originalUitstap.Route.Points += ";" + input[2];
                            else
                                originalUitstap.Route.Points += input[2];

                            ViewBag.Points = GetPoisInRoute(originalUitstap);
                            us.UpdateUitstap(originalUitstap);
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });
                        
                        case "poi":
                            originalUitstap.POI.Add(us.GetPOIById(int.Parse(input[2])));
                            us.UpdateUitstap(originalUitstap);
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });

                        case "user":
                            originalUitstap.AspNetUsers1.Add(us.GetUserById(input[2]));
                            us.UpdateUitstap(originalUitstap);
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });

                        default: return RedirectToAction("Index", new { filter = "my" });
                    }

                default: return RedirectToAction("Index", new { filter = "my" });

            }
        }

        private List<POI> GetPoisInRoute(Uitstap uitstap)
        {
            List<POI> poiInRoute = new List<POI>();
            try
            {
                foreach (string s in uitstap.Route.Points.Split(';'))
                    poiInRoute.Add(us.GetPOIById(int.Parse(s)));
            }
            catch (Exception) { }
            return poiInRoute;
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