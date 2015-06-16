namespace DigitaalOmgevingsboek
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("POI_Log")]
    public partial class POI_Log
    {
        public int Id { get; set; }
       
        public string Event { get; set; }

      
        public string Time { get; set; }
 
        public string POI_Id { get; set; }

        //public POI POI { get; set; }
    }
}
