using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Businesslayer.Services
{
    public class POIService : OmgevingsboekMVC.Businesslayer.Services.IPOIService
    {
<<<<<<< HEAD
        private IGenericRepository<POI> repoPOI = null;

        public POIService(IGenericRepository<POI> repoPOI)
        {
            this.repoPOI = repoPOI;
=======
        private IPOIRepository repoPOI = null;
        private IGenericRepository<Foto_POI> repoFotoPOI = null;
        private IGenericRepository<Doelgroep> repoDoelgroep = null;
        private IGenericRepository<Leerdoel> repoLeerdoel = null;

        public POIService(IPOIRepository repoPOI, IGenericRepository<Foto_POI> repoFotoPOI, IGenericRepository<Doelgroep> repoDoelgroep, IGenericRepository<Leerdoel> repoLeerdoel)
        {
            this.repoPOI = repoPOI;
            this.repoFotoPOI = repoFotoPOI;
            this.repoDoelgroep = repoDoelgroep;
            this.repoLeerdoel = repoLeerdoel;
>>>>>>> DataAcces
        }

        /*
        GET
        */
        public List<POI> GetPOIs()
        {
            return repoPOI.All().ToList<POI>();
        }

        public POI GetPOI(int id)
        {
            return repoPOI.GetByID(id);
        }

<<<<<<< HEAD
        public POI AddPOI(POI poi)
        {
            return repoPOI.Insert(poi);
=======
        public List<Doelgroep> GetDoelgroepen()
        {
            return repoDoelgroep.All().ToList<Doelgroep>();
        }

        public List<Leerdoel> GetLeerdoelen()
        {
            return repoLeerdoel.All().ToList<Leerdoel>();
        }

        /*
        ADD
        */
        public void AddOrUpdatePOI(POI poi)
        {
            repoPOI.Update(poi);
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
>>>>>>> DataAcces
        }
    }
}