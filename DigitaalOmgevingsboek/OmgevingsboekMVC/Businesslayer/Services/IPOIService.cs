using System;
namespace OmgevingsboekMVC.Businesslayer.Services
{
    public interface IPOIService
    {
        void AddPOI(DigitaalOmgevingsboek.POI poi);
        void UpdatePOI(DigitaalOmgevingsboek.POI poi);
        //void UpdateDoelgroep(DigitaalOmgevingsboek.Doelgroep dg);
        System.Collections.Generic.List<DigitaalOmgevingsboek.Doelgroep> GetDoelgroepen();
        System.Collections.Generic.List<DigitaalOmgevingsboek.Leerdoel> GetLeerdoelen();
        System.Collections.Generic.List<DigitaalOmgevingsboek.Thema> GetThemas();
        DigitaalOmgevingsboek.POI GetPOI(int id);
        DigitaalOmgevingsboek.Doelgroep GetDoelgroep(int doelgroepId);
        System.Collections.Generic.List<DigitaalOmgevingsboek.POI> GetPOIByDoelgroep(int doelgroepId);
        System.Collections.Generic.List<DigitaalOmgevingsboek.POI> GetPOIByThema(int themaId);
        System.Collections.Generic.List<DigitaalOmgevingsboek.POI> GetPOIs();
        void UploadPicture(DigitaalOmgevingsboek.POI poi, System.Web.HttpPostedFileBase picture);
    }
}
