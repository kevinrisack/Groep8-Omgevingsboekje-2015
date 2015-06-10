using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class LeerdoelRepository : GenericRepository<Leerdoel>
    {
        OmgevingsboekContext context;
        public LeerdoelRepository(OmgevingsboekContext context) : base(context)
        {
            this.context = context;
        }

        public override Leerdoel GetByID(object id)
        {
            var query = (from l in context.Leerdoel.Include(l => l.Activiteit)
                         where l.Id == (int)id
                         select l);
            return query.Single<Leerdoel>();
        }

        public override void Update(Leerdoel ld)
        {
            foreach (Activiteit act in ld.Activiteit)
            {
                context.Set<Activiteit>().Attach(act);
                context.Entry(act).State = EntityState.Unchanged;
            }
            base.Update(ld);
        }
    }
}