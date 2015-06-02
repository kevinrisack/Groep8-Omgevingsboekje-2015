using System;
namespace OmgevingsboekMVC.Businesslayer.Services
{
    public interface IPOIService
    {
        global::DigitaalOmgevingsboek.POI AddPOI(global::DigitaalOmgevingsboek.POI poi);
        global::DigitaalOmgevingsboek.POI GetPOI(int id);
        global::System.Collections.Generic.List<global::DigitaalOmgevingsboek.POI> GetPOIs();
    }
}
