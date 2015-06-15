using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Blob;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class ActiviteitRepository : GenericRepository<Activiteit>
    {
        OmgevingsboekContext context;
        public ActiviteitRepository(OmgevingsboekContext context)
            : base(context)
        {
            this.context = context;
        }

        public override Activiteit GetByID(object id)
        {
            var query = (from a in context.Activiteit.Include(a => a.POI)
                                                     .Include(a => a.Foto_Activiteit)
                                                     .Include(a => a.Link)
                                                     .Include(a => a.Doelgroep)
                         where a.Id == (int)id
                         select a);
            return query.Single<Activiteit>();
        }

        public void UploadPicture(Foto_Activiteit fotoActiviteit, HttpPostedFileBase picture)
        {
            //retrieve storage account from connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            //create the blob client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //retrieve reference to a previously created container
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            //retrieve reference to a blob named "pictureName"
            string pictureName = fotoActiviteit.URL;
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(pictureName);

            //create or overwrite the 'picture.FileName" blob with contents from a local file
            blockBlob.UploadFromStream(picture.InputStream);
        }

        public void DeletePicture(Foto_Activiteit fotoAct)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            // Retrieve reference to a blob named "myblob.txt".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fotoAct.URL);

            // Delete the blob.
            blockBlob.Delete();
        }
    }
}