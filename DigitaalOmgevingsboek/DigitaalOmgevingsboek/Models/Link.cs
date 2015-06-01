using System;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class Link
    {
        public int Id { get; set; }


        public string URL { get; set; }
        public int Activiteit_Id { get; set; }

        [Required(ErrorMessage = "Het URL veld is verplicht.")]
        [StringLength(50, ErrorMessage = "De URL mag maar 100 karakters bevatten.")]
        public string URL { get; set; }
     

    }
}