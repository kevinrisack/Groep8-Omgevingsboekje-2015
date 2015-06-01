namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Thema")]
    public partial class Thema
    {
        public Thema()
        {
            POI = new HashSet<POI>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage="Geef een themanaam in"]
        [StringLength(50)]
        public string ThemaNaam { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<POI> POI { get; set; }
    }
}
