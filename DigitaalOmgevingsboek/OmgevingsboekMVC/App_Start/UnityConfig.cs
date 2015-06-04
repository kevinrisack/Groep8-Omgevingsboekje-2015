using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using OmgevingsboekMVC.Controllers;
using OmgevingsboekMVC.Businesslayer.Services;
using DigitaalOmgevingsboek.BusinessLayer;
using DigitaalOmgevingsboek.Businesslayer.Services;

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
<<<<<<< HEAD
=======
            container.RegisterType<IGenericRepository<Foto_POI>, GenericRepository<Foto_POI>>();
            container.RegisterType<IGenericRepository<Doelgroep>, GenericRepository<Doelgroep>>();
            container.RegisterType<IGenericRepository<Leerdoel>, GenericRepository<Leerdoel>>();
            container.RegisterType<IPOIRepository, POIRepository>();
>>>>>>> DataAcces
            container.RegisterType<IPOIService, POIService>();

            container.RegisterType<AccountController>(new InjectionConstructor());  //Identity Model

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}