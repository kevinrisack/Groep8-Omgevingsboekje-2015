using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class POI
    {

       
        public int Id { get; set; }


         [Required]
         [StringLength(50, MinimumLength = 6, ErrorMessage = "De naam moet minimum 6 characters bevatten")]
        public string Naam { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Het adres moet minimum 6 characters bevatten")]
        [Display(Name="Straat + nummer")]
         public string Adres { get; set; }

        [Required]
        public string Gemeente { get; set; }

        public string Telefoon { get; set; }

        [EmailAddress(ErrorMessage = "U heeft een onjuist emailadres ingegeven")]
        public string Email { get; set; }

        [Display(Name="Website locatie")]
        public string WebsiteURL { get; set; }

        public string Openingsuur { get; set; }

        [DataType(DataType.Currency,ErrorMessage="Geef een geldige waarde op. vb: 2,08")]
        public float Toegangsprijs { get; set; }

        [MinLength(6,ErrorMessage="De beschrijving moet minimum 6 karakters bevatten")]
        public string Beschrijving { get; set; }

        public string Contactpersoon_Naam { get; set; }

        [EmailAddress(ErrorMessage = "U heeft een onjuist emailadres ingegeven")]
        public string Contactpersoon_Email { get; set; }

        public string Auteur_Id { get; set; }

        public DateTime TimeCreated { get; set; }
        public DateTime TimeModified { get; set; }
        public string Duurtijd { get; set; }

        public bool IsDeleted { get; set; }

      
                

    }
}