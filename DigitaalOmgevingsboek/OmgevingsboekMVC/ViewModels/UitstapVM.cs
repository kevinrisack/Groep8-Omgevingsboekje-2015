using DigitaalOmgevingsboek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmgevingsboekMVC.ViewModels
{
    public class UitstapVM
    {
        public Uitstap uitstap { get; set; }
        public List<POI> lijstPOI { get; set; }
        public List<POI> allePOI { get; set; }
    }
}