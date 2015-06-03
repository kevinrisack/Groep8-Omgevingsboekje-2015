using DigitaalOmgevingsboek;
using OmgevingsboekMVC.Businesslayer.Services;
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

        public UitstapController(UitstapService us)
        {
            this.us = us;
        }
        
        // GET: Uitstap
        public ActionResult Index()
        {
            IEnumerable<Uitstap> uitstappen = us.GetUitappen();
            return View(uitstappen);
        }

        public ActionResult Edit()
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