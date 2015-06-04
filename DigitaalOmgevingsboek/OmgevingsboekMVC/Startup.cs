using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OmgevingsboekMVC.Startup))]
namespace OmgevingsboekMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
