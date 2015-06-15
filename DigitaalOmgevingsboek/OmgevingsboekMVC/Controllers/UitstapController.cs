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
using OmgevingsboekMVC.ViewModel;

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
            if (filter == null)
                return RedirectToAction("Index", "Uitstap", new { filter = "all" });

            List<Uitstap> uitstappen = us.GetUitstappen();
            List<Uitstap> uitstappenMine = us.GetUitstappen(User.Identity.GetUserId());

            ViewBag.nMine = uitstappenMine.Count;

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
            ViewBag.User = User.Identity.GetUserId();
            
            switch(filter)
            {
                case "my":
                            ViewBag.Filter = "Mijn";
                            return View(uitstappenMine);

                case "all":
                            ViewBag.Filter = "Alle";
                            return View(uitstappenMetRechten);

                default: return RedirectToAction("Index", "Uitstap", new { filter = "all"});
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

            if(!User.IsInRole("Administrator"))
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
                return RedirectToAction("Index", new { filter = "all" });

            UitstapVM uitstapVM = new UitstapVM();
            uitstapVM.Uitstap = us.GetUitstap(id.Value);

            if (uitstapVM.Uitstap.Naam != null)
            {
                if (uitstapVM.Uitstap.Auteur_Id == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                {


                    ViewBag.POI = us.GetPOIs();
                    ViewBag.Users = us.GetUsers();
                    ViewBag.Points = GetPoisInRoute(uitstapVM.Uitstap);

                    uitstapVM.Points = new List<SelectListItem>();
                    foreach (POI poi in ViewBag.Points)
                        uitstapVM.Points.Add(new SelectListItem { Value = poi.Id.ToString(), Text = poi.Naam });

                    return View(uitstapVM);
                }
                else
                    return RedirectToAction("Index", new { filter = "all" });
            }
            else
                return RedirectToAction("Index", new { filter = "all" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UitstapVM uitstapVM, string submit)
        {
            Uitstap originalUitstap = us.GetUitstap(uitstapVM.Uitstap.Id);

            //Route punten toevoegen aan Uitstap
            if (uitstapVM.SelectedValues != null)
            {
                originalUitstap.Route.Points = uitstapVM.SelectedValues[0];
                for (int i = 1; i < uitstapVM.SelectedValues.Length; i++)
                    originalUitstap.Route.Points += ";" + uitstapVM.SelectedValues[i];
            }

            //Algemene Info toevoegen aan Uitstap
            if (originalUitstap.Naam != uitstapVM.Uitstap.Naam) originalUitstap.Naam = uitstapVM.Uitstap.Naam;
            if (originalUitstap.Beschrijving != uitstapVM.Uitstap.Beschrijving) originalUitstap.Beschrijving = uitstapVM.Uitstap.Beschrijving;
            
            string[] input = submit.Split(':');

            switch (input[0])
            {
                //Save button pressed
                case "save":
                    if (!ModelState.IsValid)
                    {
                        ViewBag.POI = us.GetPOIs();
                        ViewBag.Users = us.GetUsers();
                        ViewBag.Points = GetPoisInRoute(originalUitstap);
                        return View(uitstapVM);
                    }

                    us.UpdateUitstap(originalUitstap);
                    return RedirectToAction("Details", uitstapVM.Uitstap.Id);

                //case "direction":
                //    originalUitstap.Route.Points = ChangeDirection(originalUitstap, routelijst, input[1]);
                //    us.UpdateUitstap(originalUitstap);
                //    return RedirectToAction("Edit", new { id = originalUitstap.Id });

                case "delete":
                    switch (input[1])
                    {
                        case "route":
                            string newPoints = RouteVerwijderen(originalUitstap, input[2]);

                            originalUitstap.Route.Points = newPoints;

                            us.UpdateUitstap(originalUitstap);
                                    
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });
                        
                        case "poi":
                            originalUitstap.POI.Remove(us.GetPOIById(int.Parse(input[2])));

                            string newPoints2 = RouteVerwijderen(originalUitstap, input[2]);

                            originalUitstap.Route.Points = newPoints2;

                            us.UpdateUitstap(originalUitstap);
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });

                        case "user":
                            originalUitstap.AspNetUsers1.Remove(us.GetUserById(input[2]));
                            us.UpdateUitstap(originalUitstap);
                            return RedirectToAction("Edit", new { id = originalUitstap.Id });

                        default: return RedirectToAction("Index", new { filter = "all" });
                    }


                case "add":
                    switch(input[1])
                    {
                        case "route":
                            if (originalUitstap.Route.Points != null)
                                originalUitstap.Route.Points += ";" + input[2];
                            else
                                originalUitstap.Route.Points += input[2];

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

                        default: return RedirectToAction("Index", new { filter = "all" });
                    }

                default: return RedirectToAction("Index", new { filter = "all" });

            }
        }

        #region Custom Methods

        private string ChangeDirection(Uitstap uitstap, string routelijst, string direction)
        {
            POI movedPOI = us.GetPOIById(int.Parse(routelijst));
            List<POI> listRoute = GetPoisInRoute(uitstap);

            for (int i = 0; i < listRoute.Count; i++)
                if (listRoute[i].Id == movedPOI.Id)
                    switch(direction)
                    {
                        case "up":
                            if (i > 0)
                            {
                                listRoute.RemoveAt(i);
                                listRoute.Insert(i - 1, movedPOI);
                            }
                            break;

                        case "down":
                            if(i < listRoute.Count-1)
                            {
                                listRoute.RemoveAt(i);
                                listRoute.Insert(i+1, movedPOI);
                            }
                            break;
                    }
            return ConvertListToString(listRoute);
        }

        private string RouteVerwijderen(Uitstap uitstap, string input)
        {
            List<POI> poisInRoute = GetPoisInRoute(uitstap);
            poisInRoute.Remove(us.GetPOIById(int.Parse(input)));

            return ConvertListToString(poisInRoute);
        }

        private static string ConvertListToString(List<POI> poisInRoute)
        {
            string newPoints = null;
            if (poisInRoute.Count != 0)
            {
                newPoints = poisInRoute.First().Id.ToString(); poisInRoute.RemoveAt(0);
                foreach (POI point in poisInRoute)
                    newPoints += ";" + point.Id;
            }
            else
                newPoints = null;
            return newPoints;
        }
        #endregion

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
            if (id.HasValue)
            {
                Uitstap uitstap = us.GetUitstap(id.Value);

                if (uitstap.Auteur_Id == User.Identity.GetUserId() || User.IsInRole("Administrator"))
                {
                    uitstap.IsDeleted = true;
                    us.UpdateUitstap(uitstap);
                    return RedirectToAction("Index", new { filter = "all" });
                }
                else
                {
                    return View("Error: " + "U heeft geen toestemming op deze uitstap te verwijderen.");
                }
            }
            return RedirectToAction("Index", new { filter = "all" });
        }
    }
}