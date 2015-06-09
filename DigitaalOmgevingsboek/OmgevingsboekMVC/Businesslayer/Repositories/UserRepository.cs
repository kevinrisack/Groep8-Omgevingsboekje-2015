using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigitaalOmgevingsboek.BusinessLayer;
using DigitaalOmgevingsboek;
using System.Data.Entity;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class UserRepository : GenericRepository<AspNetUsers>
    {
        OmgevingsboekContext context;
        public UserRepository(OmgevingsboekContext context) : base(context)
        {
            this.context = context;
        }

        public override IEnumerable<AspNetUsers> All()
        {
            var query = (from u in this.context.AspNetUsers.Include(u => u.AspNetRoles)
                         where u.IsDeleted == false
                         select u);
            return query;
        }
    }
}