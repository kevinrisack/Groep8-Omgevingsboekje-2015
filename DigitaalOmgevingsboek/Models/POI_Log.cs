namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class POI_Log
    {
        [StringLength(50)]
        public string Event { get; set; }

        [Key]
        [Column(Order = 0, TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Time { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int POI_Id { get; set; }

        public virtual POI POI { get; set; }
    }
}
