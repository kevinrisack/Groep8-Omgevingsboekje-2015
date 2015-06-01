using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class Activiteit
    {
        public int Id { get; set; }
        public int Doelgroep_Id { get; set; }
        [Required(ErrorMessage = "Naam is verplicht in te vullen.")]
        [StringLength(50, ErrorMessage = "Naam mag maar 50 karakters bevatten.")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "Beschrijving is verplicht in te vullen.")]
        public string Beschrijving { get; set; }
        public int Leerdoel_Id { get; set; }
        [StringLength(50, ErrorMessage = "Duur mag maar 50 karakters bevatten.")]
        public string Duur { get; set; }
        public string Terugkoppeling { get; set; }
        public string Opmerkingen { get; set; }
        public int POI_Id { get; set; }
        public string Materiaal { get; set; }
    }
}