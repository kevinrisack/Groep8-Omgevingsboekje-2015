using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class ThemaRepository : GenericRepository<Thema>
    {
        OmgevingsboekContext context;
        public ThemaRepository(OmgevingsboekContext context) : base(context)
        {
            this.context = context;
        }

        public override Thema GetByID(object id)
        {
            var query = (from t in context.Thema.Include(t => t.POI)
                         where t.Id == (int) id
                         select t);
            return query.Single<Thema>();
        }

        public override void Update(Thema th)
        {
            foreach (POI poi in th.POI)
            {
                context.Set<POI>().Attach(poi);
                context.Entry(poi).State = EntityState.Unchanged;
            }
            base.Update(th);
        }
    }
}