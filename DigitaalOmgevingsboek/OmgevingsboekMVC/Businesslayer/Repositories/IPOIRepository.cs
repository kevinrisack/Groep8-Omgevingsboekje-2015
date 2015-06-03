using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public interface IPOIRepository : IGenericRepository<POI>
    {
        System.Collections.Generic.IEnumerable<DigitaalOmgevingsboek.POI> All();
        DigitaalOmgevingsboek.POI GetByID(object id);
        void UploadPicture(DigitaalOmgevingsboek.Foto_POI fotoPOI, System.Web.HttpPostedFileBase picture);
    }
}
