namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        public Route()
        {
            Uitstap = new HashSet<Uitstap>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Points { get; set; }

        public virtual ICollection<Uitstap> Uitstap { get; set; }
    }
}
