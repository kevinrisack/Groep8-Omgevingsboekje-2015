using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class Link
    {
        public int Id { get; set; }

        public string URL { get; set; }
        public int Activiteit_Id { get; set; }
    }
}