namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Uitstap")]
    public partial class Uitstap
    {
        public Uitstap()
        {
            AspNetUsers1 = new HashSet<AspNetUsers>();
            POI = new HashSet<POI>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Naam { get; set; }

        public int? Route_Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(128)]
        public string Auteur_Id { get; set; }

        public  AspNetUsers AspNetUsers { get; set; }

        public Route Route { get; set; }

        public ICollection<AspNetUsers> AspNetUsers1 { get; set; }

        public ICollection<POI> POI { get; set; }
    }
}
