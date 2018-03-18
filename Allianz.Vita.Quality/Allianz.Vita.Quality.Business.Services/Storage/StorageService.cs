using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System.IO;

namespace Allianz.Vita.Quality.Business.Services.Storage
{
    public class StorageService : IStorageService
    {
        IConfigurationService Conf = null;
        IIdentityService Auth = null;

        Vita.Storage.Storage Service;

        public StorageService() : this(conf: null, auth: null) { }

        public StorageService(IConfigurationService conf, IIdentityService auth)
        {
            Conf = conf ?? ServiceFactory.Get<IConfigurationService>();

            Auth = auth ?? ServiceFactory.Get<IIdentityService>();

            Service = new Vita.Storage.Storage();    

        }

        public string Store(IAttachment att, string fileName)
        {
            string fullpath = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllBytes(fullpath, att.Content);

            return fullpath;
        }
    }
}
