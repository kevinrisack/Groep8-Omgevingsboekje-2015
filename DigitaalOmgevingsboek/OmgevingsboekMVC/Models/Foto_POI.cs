namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Foto_POI
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FotoURL { get; set; }

        public int POI_Id { get; set; }

        public POI POI { get; set; }
    }
}
