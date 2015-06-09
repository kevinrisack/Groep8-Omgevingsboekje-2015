using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class GebruikerRepository: GenericRepository<AspNetUsers>
    {
        OmgevingsboekContext context;

        public GebruikerRepository(OmgevingsboekContext context):base(context)
        {
            this.context = context;

        
        
        }

        public override IEnumerable<AspNetUsers> All()
        {
            var query = (from u in this.context.AspNetUsers.Include(u=>u.POI).Include(u=>u.Uitstap).Include(u=>u.AspNetRoles)          
                         select u);
            return query.ToList<AspNetUsers>();
        }

        public AspNetUsers GetByEmail(string aspuser)
        {
            var query=(from u in this.context.AspNetUsers.Include(u=>u.POI).Include(u=>u.Uitstap).Include(u=>u.Uitstap1) where u.Email==aspuser select u);

            try
            {
                return query.Single<AspNetUsers>();
            }
            catch (Exception e)
            {
                return null;
            }
        }



        }
}