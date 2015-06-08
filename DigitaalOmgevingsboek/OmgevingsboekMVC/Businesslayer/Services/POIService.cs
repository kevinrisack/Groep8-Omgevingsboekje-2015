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
        private IGenericRepository<Foto_POI> repoFotoPOI = null;
        private DoelgroepRepository repoDoelgroep = null;
        private IGenericRepository<Leerdoel> repoLeerdoel = null;
        private IGenericRepository<Thema> repoThema = null;

        OmgevingsboekContext context;

        public POIService(POIRepository repoPOI, IGenericRepository<Foto_POI> repoFotoPOI, DoelgroepRepository repoDoelgroep, IGenericRepository<Leerdoel> repoLeerdoel, IGenericRepository<Thema> repoThema)
        {
            //this.repoPOI = repoPOI;
            //this.repoFotoPOI = repoFotoPOI;
            //this.repoDoelgroep = repoDoelgroep;
            //this.repoLeerdoel = repoLeerdoel;
            //this.repoThema = repoThema;

            this.context = new OmgevingsboekContext();

            this.repoPOI = new POIRepository(context);
            this.repoFotoPOI = repoFotoPOI;
            this.repoDoelgroep = new DoelgroepRepository(context);
            this.repoLeerdoel = repoLeerdoel;
            this.repoThema = repoThema;
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

        public List<POI> GetPOIByThema(int themaId)
        {
            return repoPOI.GetByThema(themaId);
        }

        public List<POI> GetPOIByDoelgroep(int doelgroepId)
        {
            return repoPOI.GetByDoelgroep(doelgroepId);
        }

        public List<Doelgroep> GetDoelgroepen()
        {
            return repoDoelgroep.All().ToList<Doelgroep>();
        }

        public List<Leerdoel> GetLeerdoelen()
        {
            return repoLeerdoel.All().ToList<Leerdoel>();
        }

        public List<Thema> GetThemas()
        {
            return repoThema.All().ToList<Thema>();
        }

        public Doelgroep GetDoelgroep(int doelgroepId)
        {
            return repoDoelgroep.GetByID(doelgroepId);
        }

        public void UpdateDoelgroep(Doelgroep dg)
        {
            repoDoelgroep.Update(dg);
            repoDoelgroep.SaveChanges();
        }

        /*
        ADD
        */
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
    }
}