namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Activiteit")]
    public partial class Activiteit
    {
        public Activiteit()
        {
            Foto_Activiteit = new HashSet<Foto_Activiteit>();
            Link = new HashSet<Link>();
            Leerdoel = new HashSet<Leerdoel>();
            Doelgroep = new HashSet<Doelgroep>();
        }

        public int Id { get; set; }

        public int Doelgroep_Id { get; set; }

        [Required]
        public string Beschrijving { get; set; }

        public int Leerdoel_Id { get; set; }

        public string Duur { get; set; }

        public string Terugkoppeling { get; set; }

        public string Opmerkingen { get; set; }

        public int POI_Id { get; set; }

        public string Materiaal { get; set; }

        [Required]
        [StringLength(50)]
        public string Naam { get; set; }

        public virtual POI POI { get; set; }

        public virtual ICollection<Foto_Activiteit> Foto_Activiteit { get; set; }

        public virtual ICollection<Link> Link { get; set; }

        public virtual ICollection<Leerdoel> Leerdoel { get; set; }

        public virtual ICollection<Doelgroep> Doelgroep { get; set; }
    }
}
