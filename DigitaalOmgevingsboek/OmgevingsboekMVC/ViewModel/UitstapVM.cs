using DigitaalOmgevingsboek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmgevingsboekMVC.ViewModel
{
    public class UitstapVM
    {
        public Uitstap Uitstap { get; set; }
        public string[] SelectedValues { get; set; }
        public List<SelectListItem> Points { get; set; }
    }
}