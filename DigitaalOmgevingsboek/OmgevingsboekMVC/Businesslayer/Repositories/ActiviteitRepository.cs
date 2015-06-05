using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class ActiviteitRepository : GenericRepository<Activiteit>
    {
        //public override IEnumerable<Activiteit> All()
        //{
        //    using (OmgevingsboekContext context = new OmgevingsboekContext())
        //    {
        //        var query = (from a in context.Activiteit.Include(p => p.Activiteit)
        //                                          .Include(p => p.AspNetUsers)
        //                                          .Include(p => p.Foto_POI)
        //                                          .Include(p => p.POI_Log)
        //                                          .Include(p => p.Rating)
        //                                          .Include(p => p.Doelgroep)
        //                                          .Include(p => p.Thema)
        //                                          .Include(p => p.Uitstap)
        //                     where p.IsDeleted == false
        //                     select p);
        //        return query.ToList<POI>();
        //    }
        //}
    }
}