using DigitaalOmgevingsboek;
using OmgevingsboekMVC.Businesslayer.Repositories;
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

        public UitstapService(UitstapRepository repoUitstap, POIRepository repoPOI)
        {
            this.repoUitstap = repoUitstap;
            this.repoPOI = repoPOI;
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

        public void AddUitstap(Uitstap uitstap)
        {
            repoUitstap.Insert(uitstap);
            repoUitstap.SaveChanges();
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
    }
}