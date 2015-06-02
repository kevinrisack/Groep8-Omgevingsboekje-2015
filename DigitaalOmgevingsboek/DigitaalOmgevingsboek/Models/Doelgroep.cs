namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Doelgroep")]
    public partial class Doelgroep
    {
        public Doelgroep()
        {
            Activiteit = new HashSet<Activiteit>();
            POI = new HashSet<POI>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DoelgroepNaam { get; set; }

        public ICollection<Activiteit> Activiteit { get; set; }

        public ICollection<POI> POI { get; set; }
    }
}
