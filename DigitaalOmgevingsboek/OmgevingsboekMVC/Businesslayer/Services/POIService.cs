using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Businesslayer.Services
{
    public class POIService : DigitaalOmgevingsboek.Businesslayer.Services.IPOIService
    {
        private IGenericRepository<POI> repoPOI = null;

        public POIService(IGenericRepository<POI> repoPOI)
        {
            this.repoPOI = repoPOI;
        }

        public List<POI> GetPOIs()
        {
            return repoPOI.All().ToList<POI>();
        }
    }
}