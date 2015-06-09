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
        private ThemaRepository repoThema = null;
        private ActiviteitRepository repoActiviteit = null;

        private IGenericRepository<Foto_POI> repoFotoPOI = null;      
        private IGenericRepository<Leerdoel> repoLeerdoel = null;

        OmgevingsboekContext context;

        public POIService(IGenericRepository<Foto_POI> repoFotoPOI, IGenericRepository<Leerdoel> repoLeerdoel)
        {
            this.context = new OmgevingsboekContext();

            this.repoPOI = new POIRepository(context);
            this.repoDoelgroep = new DoelgroepRepository(context);
            this.repoThema = new ThemaRepository(context);
            this.repoActiviteit = new ActiviteitRepository(context);

            this.repoFotoPOI = repoFotoPOI;          
            this.repoLeerdoel = repoLeerdoel;
        }

        #region POI
        public List<POI> GetPOIs()
        {
            return repoPOI.All().ToList<POI>();
        }  
        public List<POI> GetPOIByThema(int themaId)
        {
            return repoPOI.GetByThema(themaId);
        }
        public List<POI> GetPOIByDoelgroep(int doelgroepId)
        {
            return repoPOI.GetByDoelgroep(doelgroepId);
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

        public void UpdatePOI(POI poi)
        {
            repoPOI.Update(poi);
            repoPOI.SaveChanges();
        }
        
        public void UploadPicturePOI(POI poi, HttpPostedFileBase picture)
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

        #region Doelgroep/Leerdoel/Thema
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
        public Thema GetThema(int themaId)
        {
            return repoThema.GetByID(themaId);
        }
        
        public void UpdateDoelgroep(Doelgroep dg)
        {
            repoDoelgroep.Update(dg);
            repoDoelgroep.SaveChanges();
        }
        public void UpdateThema(Thema th)
        {
            repoThema.Update(th);
            repoThema.SaveChanges();
        }
        #endregion

        #region Activiteit
        public Activiteit GetActiviteit(int id)
        {
            return repoActiviteit.GetByID(id);
        }

        public void AddActiviteit(Activiteit act)
        {
            repoActiviteit.Insert(act);
            repoActiviteit.SaveChanges();
        }

        public void UpdateActiviteit(Activiteit act)
        {
            repoActiviteit.Update(act);
            repoActiviteit.SaveChanges();
        }

        public void UploadPictureActiviteit(Activiteit activiteit, HttpPostedFileBase picture)
        {
            Foto_Activiteit fotoActiviteit = new Foto_Activiteit()
            {
                Activiteit = activiteit,
                Activiteit_Id = activiteit.Id,
                URL = Guid.NewGuid().ToString()
            };

            activiteit.Foto_Activiteit.Add(fotoActiviteit);
            repoActiviteit.UploadPicture(fotoActiviteit, picture);

            repoActiviteit.SaveChanges();
        }
        #endregion
    }
}