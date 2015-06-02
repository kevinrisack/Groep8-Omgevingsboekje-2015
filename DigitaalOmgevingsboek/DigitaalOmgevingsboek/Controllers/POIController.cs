<<<<<<< HEAD
﻿using DigitaalOmgevingsboek.Businesslayer.Services;
using System;
=======
﻿using System;
>>>>>>> Layout
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

<<<<<<< HEAD
namespace DigitaalOmgevingsboek.Controllers
{
    public class POIController : Controller
    {
        private IPOIService ps;

        public POIController(IPOIService ps)
        {
            this.ps = ps;
        }
        public ActionResult Index()
        {
            List<POI> pois = ps.GetPOIs();
            return View(pois);
=======
namespace BootstrapSite1.Controllers
{
    public class POIController : Controller
    {
        // GET: POI
        public ActionResult POIStart()
        {
            return View();
        }

        public ActionResult POIOverzicht()
        {
            return View();
        }

        public ActionResult POINewModify()
        {
            return View();
        }


        public ActionResult POIActivity()
        {
            return View();
        }


        public ActionResult POIView()
        {
            return View();
>>>>>>> Layout
        }
    }
}