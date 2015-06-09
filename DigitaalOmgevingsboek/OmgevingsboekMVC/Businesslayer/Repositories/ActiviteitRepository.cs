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

        //public List<Activiteit> GetActiviteiten(POI poi)
        //{
        //    var query = (from a in context.Activiteit.Include(a => a.POI)
        //                                             .Include(a => a.Foto_Activiteit)
        //                                             .Include(a => a.Link)
        //                                             .Include(a=> a.Leerdoel)
        //                 where a.POI == poi
        //                 select a);
        //    return query.ToList<Activiteit>();
        //}

        public override Activiteit GetByID(object id)
        {
            var query = (from a in context.Activiteit.Include(a => a.POI)
                                                     .Include(a => a.Foto_Activiteit)
                                                     .Include(a => a.Link)
                                                     .Include(a => a.Leerdoel)
                                                     .Include(a => a.Doelgroep)
                         where a.Id == (int)id
                         select a);
            return query.Single<Activiteit>();
        }
    }
}