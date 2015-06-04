using DigitaalOmgevingsboek;
using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmgevingsboekMVC.Controllers
{
    public class UitstapController : Controller
    {
        UitstapService us;
        GenericRepository<AspNetUsers> usUsers;

        public UitstapController(UitstapService us, GenericRepository<AspNetUsers> usUsers)
        {
            this.us = us;
            this.usUsers = usUsers;
        }
        
        // GET: Uitstap
        public ActionResult Index()
        {
            IEnumerable<Uitstap> uitstappen = us.GetUitappen();
            ViewBag.Users = usUsers.All();
            return View(uitstappen);
        }

        public ActionResult New()
        {
            Uitstap uitstap = new Uitstap();
            return View(uitstap);
        }

        public ActionResult Edit(int id)
        {
            Uitstap uitstap = us.GetUitstap(id);
            return View(uitstap);
        }

        public ActionResult Details(int id)
        {
            Uitstap uitstap = us.GetUitstap(id);
            return View(uitstap);
        }

        public ActionResult Delete(int id)
        {
            Uitstap uitstap = us.GetUitstap(id);
            return View(uitstap);
        }
    }
}