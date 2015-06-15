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

        [Required]
        [StringLength(50)]
        public string LeergebiedNaam { get; set; }

        public string DomeinNaam { get; set; }

        public bool IsDeleted { get; set; }

        public  ICollection<POI> POI { get; set; }
    }
}
