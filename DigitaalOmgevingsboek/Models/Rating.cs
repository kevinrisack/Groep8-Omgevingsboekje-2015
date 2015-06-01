namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        public int Id { get; set; }

        public int? Score { get; set; }

        [Required(ErrorMessage="U moeten een score geven")]
        [StringLength(50)]
        public string Comment { get; set; }

        public int? POI_Id { get; set; }

        public int Gebruiker_Id { get; set; }

        public virtual POI POI { get; set; }
    }
}
