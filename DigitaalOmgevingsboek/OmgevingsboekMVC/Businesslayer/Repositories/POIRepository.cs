using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public class POIRepository : GenericRepository<POI>, OmgevingsboekMVC.Businesslayer.Repositories.IPOIRepository
    {
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