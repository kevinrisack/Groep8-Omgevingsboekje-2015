using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class UitstapRepository : GenericRepository<Uitstap>
    {
        OmgevingsboekContext context;
        public UitstapRepository(OmgevingsboekContext context) : base(context)
        {
            this.context = context;
        }
        
        public override IEnumerable<Uitstap> All()
        {
            var query = (from u in this.context.Uitstap.Include(u => u.AspNetUsers1)
                         where u.IsDeleted == false
                         select u);
            return query;
        }
    }
}