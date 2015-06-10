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

        [Required]
        public int? Score { get; set; }

        [Required]
        [StringLength(512)]
        public string Comment { get; set; }

        public int? POI_Id { get; set; }

        public int Gebruiker_Id { get; set; }

        public POI POI { get; set; }
    }
}
