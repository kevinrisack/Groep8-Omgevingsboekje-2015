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
           
            Doelgroep = new HashSet<Doelgroep>();
        }

        public int Id { get; set; }

        public int Doelgroep_Id { get; set; }

        [Required(ErrorMessage="Beschrijving is verplicht in te vullen")]
        public string Beschrijving { get; set; }

       

        [StringLength(50,ErrorMessage="Duur mag maar 50 karakters bevatten")]
        public string Duur { get; set; }

        public string Terugkoppeling { get; set; }

        public string Opmerkingen { get; set; }

        public int POI_Id { get; set; }

        public string Materiaal { get; set; }

        [Required(ErrorMessage="Naam is verplicht in te vullen")]
        [StringLength(50,ErrorMessage="Naam mag maar 50 karakters bevatten")]
        public string Naam { get; set; }

        public  POI POI { get; set; }

        public ICollection<Foto_Activiteit> Foto_Activiteit { get; set; }

        public ICollection<Link> Link { get; set; }

       

        public ICollection<Doelgroep> Doelgroep { get; set; }
    }
}
