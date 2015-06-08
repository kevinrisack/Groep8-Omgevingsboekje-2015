using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class POIRepository : GenericRepository<POI>
    {
        OmgevingsboekContext context;
        public POIRepository(OmgevingsboekContext context)
            : base(context)
        {
            this.context = context;
        }

        public override IEnumerable<POI> All()
        {
            var query = (from p in context.POI.Include(p => p.Activiteit)
                                              .Include(p => p.AspNetUsers)
                                              .Include(p => p.Foto_POI)
                                              .Include(p => p.POI_Log)
                                              .Include(p => p.Rating)
                                              .Include(p => p.Doelgroep)
                                              .Include(p => p.Thema)
                                              .Include(p => p.Uitstap)
                         where p.IsDeleted == false
                         select p);
            return query.ToList<POI>();
        }

        public override POI GetByID(object id)
        {
            var query = (from p in context.POI.Include(p => p.Activiteit)
                                              .Include(p => p.AspNetUsers)
                                              .Include(p => p.Foto_POI)
                                              .Include(p => p.POI_Log)
                                              .Include(p => p.Rating)
                                              .Include(p => p.Doelgroep)
                                              .Include(p => p.Thema)
                                              .Include(p => p.Uitstap)
                         where p.IsDeleted == false && p.Id == (int) id
                         select p);
            return query.Single<POI>();
        }

        public List<POI> GetByThema(int themaId)
        {
            var query = (from p in context.POI.Include(p => p.Activiteit)
                                              .Include(p => p.AspNetUsers)
                                              .Include(p => p.Foto_POI)
                                              .Include(p => p.POI_Log)
                                              .Include(p => p.Rating)
                                              .Include(p => p.Doelgroep)
                                              .Include(p => p.Thema)
                                              .Include(p => p.Uitstap)
                         where p.IsDeleted == false && p.Thema.Any(t => t.Id == themaId)
                         select p);
            return query.ToList<POI>();
        }

        public List<POI> GetByDoelgroep(int doelgroepId)
        {
            var query = (from p in context.POI.Include(p => p.Activiteit)
                                              .Include(p => p.AspNetUsers)
                                              .Include(p => p.Foto_POI)
                                              .Include(p => p.POI_Log)
                                              .Include(p => p.Rating)
                                              .Include(p => p.Doelgroep)
                                              .Include(p => p.Thema)
                                              .Include(p => p.Uitstap)
                         where p.IsDeleted == false && p.Doelgroep.Any(d => d.Id == doelgroepId)
                         select p);
             return query.ToList<POI>();
        }

        public void UploadPicture(Foto_POI fotoPOI, HttpPostedFileBase picture)
        {
            //retrieve storage account from connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //create the blob client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //retrieve reference to a previously created container
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            //retrieve reference to a blob named "pictureName"
            string pictureName = fotoPOI.FotoURL;
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(pictureName);

            //create or overwrite the 'picture.FileName" blob with contents from a local file
            blockBlob.UploadFromStream(picture.InputStream);
        }
    }
}