using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "U moet een score geven van 1 t.e.m. 5")]
        public int Score { get; set; }
        [Required(ErrorMessage = "U moet een comment opgegeven.")]
        public string Comment { get; set; }
        public int POI_Id { get; set; }
        public int Gebruiker_Id { get; set; }
    }
}