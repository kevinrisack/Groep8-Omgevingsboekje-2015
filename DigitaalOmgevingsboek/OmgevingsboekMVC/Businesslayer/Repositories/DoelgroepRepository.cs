using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class DoelgroepRepository : GenericRepository<Doelgroep>
    {
        OmgevingsboekContext context;
        public DoelgroepRepository(OmgevingsboekContext context) : base(context)
        {
            this.context = context;
        }

        public override Doelgroep GetByID(object id)
        {
            var query = (from d in context.Doelgroep.Include(d => d.Activiteit)
                                                    .Include(d => d.POI)
                         where d.Id == (int) id
                         select d);
            return query.Single<Doelgroep>();
        }

        //public void UpdateDoelgroep(Doelgroep dg)
        //{
        //    //context.Entry(dg).State = EntityState.Unchanged;
        //    repoDoelgroep.Update(dg);
        //    repoDoelgroep.SaveChanges();
        //}

        public override void Update(Doelgroep dg)
        {
            foreach (POI poi in dg.POI)
            {
                context.Set<POI>().Attach(poi);
                context.Entry(poi).State = EntityState.Unchanged;
            }
            base.Update(dg);
            //context.Set<POI>().Attach(dg.POI);
            //context.
            //if (context.Entry(dg.POI).State == EntityState.Detached)
            //{
            //    dbSet.Attach(dg);
            //}

            //base.Update(dg);
        }
    }
}