using DigitaalOmgevingsboek;
using OmgevingsboekMVC.Businesslayer.Repositories;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmgevingsboekMVC.Businesslayer.Services
{
    public class UitstapService
    {
        private UitstapRepository repoUitstap = null;
        private POIRepository repoPOI = null;
        private UserRepository repoUser = null;

        private OmgevingsboekContext context;

        public UitstapService()
        {
            this.context = new OmgevingsboekContext();

            this.repoUitstap = new UitstapRepository(context);
            this.repoPOI = new POIRepository(context);
            this.repoUser = new UserRepository(context);
        }

        #region Uitstappen
        public List<Uitstap> GetUitstappen()
        {
            return repoUitstap.All().ToList<Uitstap>();
        }

        public List<Uitstap> GetUitstappen(string userId)
        {
            List<Uitstap> uitstappen = GetUitstappen();
            List<Uitstap> uitstappenWithOwner = new List<Uitstap>();

            foreach(Uitstap u in uitstappen)
            {
                if (u.AspNetUsers.Id == userId)
                    uitstappenWithOwner.Add(u);
            }

            return uitstappenWithOwner;
        }

        public Uitstap GetUitstap(int id)
        {
            return repoUitstap.GetByID(id);
        }

        public Uitstap AddUitstap(Uitstap uitstap)
        {
            Uitstap result = repoUitstap.Insert(uitstap);
            repoUitstap.SaveChanges();
            return result;
        }

        public void UpdateUitstap(Uitstap uitstap)
        {
            repoUitstap.Update(uitstap);
            repoUitstap.SaveChanges();
        }

        public void DeleteUitstap(int id)
        {
            repoUitstap.Delete(id);
            repoUitstap.SaveChanges();
        }
        #endregion

        #region POI
        public List<POI> GetPOIs()
        {
            return repoPOI.All().ToList<POI>();
        }

        public POI GetPOIById(int id)
        {
            return repoPOI.GetByID(id);
        }
        #endregion

        #region Users
        public List<AspNetUsers> GetUsers()
        {
            return repoUser.All().ToList<AspNetUsers>();
        }
        public AspNetUsers GetUserById(string id)
        {
            return repoUser.GetByID(id);
        }
        #endregion
    }
}