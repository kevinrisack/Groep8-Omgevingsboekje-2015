using DigitaalOmgevingsboek;
using DigitaalOmgevingsboek.BusinessLayer;
using System;
namespace OmgevingsboekMVC.Businesslayer.Repositories
{
    public interface IPOIRepository : IGenericRepository<POI>
    {
        System.Collections.Generic.IEnumerable<DigitaalOmgevingsboek.POI> All();
        System.Collections.Generic.List<DigitaalOmgevingsboek.POI> GetByDoelgroep(int doelgroepId);
        DigitaalOmgevingsboek.POI GetByID(object id);
        System.Collections.Generic.List<DigitaalOmgevingsboek.POI> GetByThema(int themaId);
        void UploadPicture(DigitaalOmgevingsboek.Foto_POI fotoPOI, System.Web.HttpPostedFileBase picture);
        void UpdateDoelgroep(DigitaalOmgevingsboek.Doelgroep dg);
    }
}
