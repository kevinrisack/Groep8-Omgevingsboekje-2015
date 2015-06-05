using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class ActiviteitRepository : GenericRepository<Activiteit>
    {
        OmgevingsboekContext context;
        public ActiviteitRepository(OmgevingsboekContext context)
            : base(context)
        {
            this.context = context;
        }

        public List<Activiteit> GetActiviteiten(POI poi)
        {
            var query = (from ac in context.Activiteit.Include(ac => ac.POI)
                                                      .Include(ac => ac.Foto_Activiteit)
                                                      .Include(ac => ac.Link)
                                                      .Include(ac=> ac.Leerdoel)
                         where ac.POI == poi
                         select ac);
            return query.ToList<Activiteit>();
        }
    }
}