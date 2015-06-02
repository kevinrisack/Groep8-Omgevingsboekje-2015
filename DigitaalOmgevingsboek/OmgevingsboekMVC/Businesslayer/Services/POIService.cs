using DigitaalOmgevingsboek.BusinessLayer;
using OmgevingsboekMVC.Businesslayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Businesslayer.Services
{
    public class POIService : OmgevingsboekMVC.Businesslayer.Services.IPOIService
    {
        private IPOIRepository repoPOI = null;
        private IGenericRepository<Foto_POI> repoFotoPOI = null;

        public POIService(IPOIRepository repoPOI, IGenericRepository<Foto_POI> repoFotoPOI)
        {
            this.repoPOI = repoPOI;
            this.repoFotoPOI = repoFotoPOI;
        }

        public List<POI> GetPOIs()
        {
            return repoPOI.All().ToList<POI>();
        }

        public POI GetPOI(int id)
        {
            return repoPOI.GetByID(id);
        }

        public void AddPOI(POI poi)
        {
            repoPOI.Insert(poi);
            repoPOI.SaveChanges();
        }

        public void UploadPicture(POI poi, HttpPostedFileBase picture)
        {
            Foto_POI fotoPOI = new Foto_POI()
            {
                POI = poi,
                POI_Id = poi.Id,
                FotoURL = Guid.NewGuid().ToString()
            };

            //repoFotoPOI.Insert(fotoPOI);
            poi.Foto_POI.Add(fotoPOI);
            repoPOI.UploadPicture(fotoPOI, picture);

            repoPOI.SaveChanges();   
        }
    }
}