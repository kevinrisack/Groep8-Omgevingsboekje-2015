using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public interface IPOIRepository : IGenericRepository<POI>
    {
        void UploadPicture(DigitaalOmgevingsboek.Foto_POI fotoPOI, System.Web.HttpPostedFileBase picture);
    }
}
