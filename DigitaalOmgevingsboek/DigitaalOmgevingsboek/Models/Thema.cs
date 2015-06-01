using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Models
{
    public class Thema
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "U moet een naam opgeven.")]
        [Display(Name = "Themanaam")]
        [StringLength(50, ErrorMessage = "De naam mag maar 50 karakters bevatten.")]
        public string ThemaNaam { get; set; }
        public bool IsDeleted { get; set; }
    }
}