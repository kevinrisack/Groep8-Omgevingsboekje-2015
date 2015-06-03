using System;
namespace OmgevingsboekMVC.Businesslayer.Services
{
    public interface IPOIService
    {
        void AddPOI(DigitaalOmgevingsboek.POI poi);
        DigitaalOmgevingsboek.POI GetPOI(int id);
        System.Collections.Generic.List<DigitaalOmgevingsboek.POI> GetPOIs();
        void UploadPicture(DigitaalOmgevingsboek.POI poi, System.Web.HttpPostedFileBase picture);
    }
}
