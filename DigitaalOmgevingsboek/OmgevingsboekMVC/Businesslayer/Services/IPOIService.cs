using System;
namespace OmgevingsboekMVC.Businesslayer.Services
{
    public interface IPOIService
    {
        void AddOrUpdatePOI(DigitaalOmgevingsboek.POI poi);
        System.Collections.Generic.List<DigitaalOmgevingsboek.Doelgroep> GetDoelgroepen();
        System.Collections.Generic.List<DigitaalOmgevingsboek.Leerdoel> GetLeerdoelen();
        DigitaalOmgevingsboek.POI GetPOI(int id);
        System.Collections.Generic.List<DigitaalOmgevingsboek.POI> GetPOIs();
        void UploadPicture(DigitaalOmgevingsboek.POI poi, System.Web.HttpPostedFileBase picture);
    }
}
