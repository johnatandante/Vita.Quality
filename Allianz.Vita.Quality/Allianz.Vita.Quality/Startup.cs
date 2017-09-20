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
        }
    }
}
