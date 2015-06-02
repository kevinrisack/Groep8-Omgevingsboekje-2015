namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Foto_Activiteit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string URL { get; set; }

        public int Activiteit_Id { get; set; }

        public virtual Activiteit Activiteit { get; set; }
    }
}
