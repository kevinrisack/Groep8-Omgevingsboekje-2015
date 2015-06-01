using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class Uitstap
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naam is verplicht in te vullen.")]
        [StringLength(50, ErrorMessage = "Naam mag maar 50 karakters bevatten.")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "Beschrijving is verplicht in te vullen.")]
        public string Beschrijving { get; set; }
        public int Route_Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Auteur_Id { get; set; }
    }
}