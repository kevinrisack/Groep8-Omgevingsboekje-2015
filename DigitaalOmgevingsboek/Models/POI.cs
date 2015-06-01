namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("POI")]
    public partial class POI
    {
        public POI()
        {
            Foto_POI = new HashSet<Foto_POI>();
            POI_Log = new HashSet<POI_Log>();
            Rating = new HashSet<Rating>();
            Doelgroep = new HashSet<Doelgroep>();
            Thema = new HashSet<Thema>();
            Uitstap = new HashSet<Uitstap>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Naam { get; set; }

        [Required]
        public string Adres { get; set; }

        public string Gemeente { get; set; }

        [StringLength(50)]
        public string Telefoon { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string WebsiteUrl { get; set; }

        [StringLength(50)]
        public string Openingsuur { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Toegangsprijs { get; set; }

        public string Beschrijving { get; set; }

        public string Contactpersoon_Naam { get; set; }

        public string Contactpersoon_Email { get; set; }

        [Required]
        [StringLength(128)]
        public string Auteur_Id { get; set; }

        public TimeSpan TimeCreated { get; set; }

        public TimeSpan TimeModified { get; set; }

        [Required]
        [StringLength(50)]
        public string Duurtijd { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Activiteit Activiteit { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<Foto_POI> Foto_POI { get; set; }

        public virtual ICollection<POI_Log> POI_Log { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }

        public virtual ICollection<Doelgroep> Doelgroep { get; set; }

        public virtual ICollection<Thema> Thema { get; set; }

        public virtual ICollection<Uitstap> Uitstap { get; set; }
    }
}
