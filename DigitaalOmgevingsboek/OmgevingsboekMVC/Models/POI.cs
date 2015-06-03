namespace DigitaalOmgevingsboek
{
    using DataAnnotationsExtensions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [StringLength(50,MinimumLength=6,ErrorMessage="De naam moet minimum 6 karakters bevatten en maximum 50")]
        public string Naam { get; set; }

        [Required]
        [DisplayName("Adres(Straat + nummer)")]
        [StringLength(50,MinimumLength=6,ErrorMessage="Het adres moet minimum 6 karakters bevatten en maximum 50")]
        public string Adres { get; set; }

        public string Gemeente { get; set; }

        [StringLength(50)]
        public string Telefoon { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage="U heeft een onjuist emailadres ingevoerd")]
        public string Email { get; set; }

        [StringLength(50)]
        [DisplayName("Website")]
        public string WebsiteUrl { get; set; }

        [StringLength(50)]
        public string Openingsuur { get; set; }

       [Numeric(ErrorMessage="Geef een geldige waarde op, bv: 1.08")]
        public decimal? Toegangsprijs { get; set; }
        [MinLength(6,ErrorMessage="De beschrijving moet minimum 6 karakters bevatten")]
        public string Beschrijving { get; set; }

        public string Contactpersoon_Naam { get; set; }

        [EmailAddress(ErrorMessage="U heeft een onjuist emailadres ingevoerd")]
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

        public Activiteit Activiteit { get; set; }

        public  AspNetUsers AspNetUsers { get; set; }

        public  ICollection<Foto_POI> Foto_POI { get; set; }

        public  ICollection<POI_Log> POI_Log { get; set; }

        public  ICollection<Rating> Rating { get; set; }

        public ICollection<Doelgroep> Doelgroep { get; set; }

        public  ICollection<Thema> Thema { get; set; }

        public ICollection<Uitstap> Uitstap { get; set; }
    }
}
