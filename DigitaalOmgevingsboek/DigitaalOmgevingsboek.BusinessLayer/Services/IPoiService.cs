using System;
namespace DigitaalOmgevingsboek.BusinessLayer.Services
{
    public interface IPoiService
    {
        System.Collections.Generic.List<DigitaalOmgevingsboek.POI> GetPOIs();
    }
}
