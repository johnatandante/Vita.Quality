using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Services;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Allianz.Vita.Quality.Startup))]
namespace Allianz.Vita.Quality
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            ServiceFactory.Register<IItemFactory, ItemFactory>();
            ServiceFactory.Register<IMailService, MailService>();
            ServiceFactory.Register<IDefectService, DefectService>();

        }
    }
}
