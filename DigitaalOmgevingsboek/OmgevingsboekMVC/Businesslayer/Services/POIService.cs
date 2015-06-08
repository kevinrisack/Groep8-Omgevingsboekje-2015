using DigitaalOmgevingsboek.BusinessLayer;
using OmgevingsboekMVC.Businesslayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DigitaalOmgevingsboek.Businesslayer.Services
{
    public class POIService
    {
        private POIRepository repoPOI = null;
        private DoelgroepRepository repoDoelgroep = null;

        private IGenericRepository<Foto_POI> repoFotoPOI = null;      
        private IGenericRepository<Leerdoel> repoLeerdoel = null;
        private IGenericRepository<Thema> repoThema = null;

        OmgevingsboekContext context;

        public POIService(POIRepository repoPOI, DoelgroepRepository repoDoelgroep, IGenericRepository<Foto_POI> repoFotoPOI, IGenericRepository<Leerdoel> repoLeerdoel, IGenericRepository<Thema> repoThema)
        {
            this.context = new OmgevingsboekContext();

            this.repoPOI = new POIRepository(context);
            this.repoDoelgroep = new DoelgroepRepository(context);

            this.repoFotoPOI = repoFotoPOI;
            this.repoFotoPOI = repoFotoPOI;            
            this.repoLeerdoel = repoLeerdoel;
            this.repoThema = repoThema;
        }

        #region Get POI(s)
        public List<POI> GetPOIs()
        {
            return repoPOI.All().ToList<POI>();
        }         
        public POI GetPOI(int id)
        {
            return repoPOI.GetByID(id);
        }

        public List<POI> GetPOIByThema(int themaId)
        {
            return repoPOI.GetByThema(themaId);
        }
        public List<POI> GetPOIByDoelgroep(int doelgroepId)
        {
            return repoPOI.GetByDoelgroep(doelgroepId);
        }
        #endregion

        #region Get doelgroep/leerdoel/thema
        public List<Doelgroep> GetDoelgroepen()
        {
            return repoDoelgroep.All().ToList<Doelgroep>();
        }
        public Doelgroep GetDoelgroep(int doelgroepId)
        {
            return repoDoelgroep.GetByID(doelgroepId);
        }

        public List<Leerdoel> GetLeerdoelen()
        {
            return repoLeerdoel.All().ToList<Leerdoel>();
        }

        public List<Thema> GetThemas()
        {
            return repoThema.All().ToList<Thema>();
        }
        #endregion

        #region Add/Update
        public void AddPOI(POI poi)
        {
            repoPOI.Insert(poi);
            repoPOI.SaveChanges();
        }

        public void UpdatePOI(POI poi)
        {
            repoPOI.Update(poi);
            repoPOI.SaveChanges();
        }
        
        public void UpdateDoelgroep(Doelgroep dg)
        {
            repoDoelgroep.Update(dg);
            repoDoelgroep.SaveChanges();
        }

        public void UploadPicture(POI poi, HttpPostedFileBase picture)
        {
            Foto_POI fotoPOI = new Foto_POI()
            {
                POI = poi,
                POI_Id = poi.Id,
                FotoURL = Guid.NewGuid().ToString()
            };

            poi.Foto_POI.Add(fotoPOI);
            repoPOI.UploadPicture(fotoPOI, picture);

            repoPOI.SaveChanges();   
        }
        #endregion
    }
}