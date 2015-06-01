using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> Stashed changes
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class Link
    {
        public int Id { get; set; }
<<<<<<< Updated upstream

        public string URL { get; set; }
        public int Activiteit_Id { get; set; }
=======
        [Required(ErrorMessage = "Het URL veld is verplicht.")]
        [StringLength(50, ErrorMessage = "De URL mag maar 100 karakters bevatten.")]
        public string URL { get; set; }
        public int POI_Id { get; set; }
>>>>>>> Stashed changes
    }
}