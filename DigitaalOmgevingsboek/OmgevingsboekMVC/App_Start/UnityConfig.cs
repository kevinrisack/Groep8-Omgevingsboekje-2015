using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using OmgevingsboekMVC.Controllers;
using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
using DigitaalOmgevingsboek.Businesslayer.Services;
using OmgevingsboekMVC.Businesslayer.Repositories;

namespace DigitaalOmgevingsboek
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IGenericRepository<POI>, GenericRepository<POI>>();
            container.RegisterType<IGenericRepository<Foto_POI>, GenericRepository<Foto_POI>>();
            container.RegisterType<IPOIRepository, POIRepository>();
            container.RegisterType<IPOIService, POIService>();

            container.RegisterType<AccountController>(new InjectionConstructor());  //Identity Model

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}