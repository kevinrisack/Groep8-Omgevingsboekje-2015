using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class Foto_POI
    {
        public int Id { get; set; }
        public string FotoURL { get; set; }
        public int POI_Id { get; set; }
    }
}