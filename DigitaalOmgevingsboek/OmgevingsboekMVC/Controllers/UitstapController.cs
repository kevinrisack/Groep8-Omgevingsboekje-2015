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
    }
}