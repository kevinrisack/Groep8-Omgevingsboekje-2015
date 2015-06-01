namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Leerdoel")]
    public partial class Leerdoel
    {
        public Leerdoel()
        {
            Activiteit = new HashSet<Activiteit>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string LeerdoelNaam { get; set; }

        public virtual ICollection<Activiteit> Activiteit { get; set; }
    }
}
