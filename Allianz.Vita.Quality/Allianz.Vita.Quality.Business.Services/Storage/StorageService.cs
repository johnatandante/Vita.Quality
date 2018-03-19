using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
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

        public IConfigurationService GetConfiguration()
        {
            return Service.GetConfiguration();

        }

        public bool Store(IIssueConfiguration item)
        {
            bool result = false;
            try
            {
                result = Service.SaveIssue(item);
            }catch(Exception e)
            {
                Console.Write("Error on save item {0}: {1}", item.ServiceName, e.Message);
            }
            
            return result;

        }

        public string Store(IAttachment att, string fileName)
        {
            string fullpath = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllBytes(fullpath, att.Content);

            return fullpath;
        }
    }
}
