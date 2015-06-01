using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitaalOmgevingsboek.BusinessLayer.Services
{
    public class PoiService : DigitaalOmgevingsboek.BusinessLayer.Services.IPoiService
    {
        private IGenericRepository<POI> repoPOI = null;

        public PoiService(IGenericRepository<POI> repoPOI)
        {
            this.repoPOI = repoPOI;
        }

        public List<POI> GetPOIs()
        {
            return repoPOI.All().ToList<POI>();
        }
    }
}
