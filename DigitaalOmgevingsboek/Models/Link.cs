namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Link")]
    public partial class Link
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string URL { get; set; }

        public int Activiteit_Id { get; set; }

        public virtual Activiteit Activiteit { get; set; }
    }
}
