using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Services;
using Allianz.Vita.Quality.Business.Services.Mail;
using Allianz.Vita.Quality.Services;
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

            ServiceFactory.Register<IConfigurationService, ConfigurationService>();
            ServiceFactory.Register<IStorageService, StorageService>();
            ServiceFactory.Register<IItemFactory, ItemFactory>();
            ServiceFactory.Register<IMailService, ExchangeMailService>();
            ServiceFactory.Register<IDefectService, TfsDefectService>();

        }
    }
}
