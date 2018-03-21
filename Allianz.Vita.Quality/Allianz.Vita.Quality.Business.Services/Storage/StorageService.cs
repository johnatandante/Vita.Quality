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

        Vita.Storage.Storage Service {
            get
            {
                return new Vita.Storage.Storage();
            }
        }
        
        public StorageService() : this(conf: null, auth: null) { }

        public StorageService(IConfigurationService conf, IIdentityService auth)
        {
            Conf = conf ?? ServiceFactory.Get<IConfigurationService>();

            Auth = auth ?? ServiceFactory.Get<IIdentityService>();
            
            InitConf();
        }

        private void InitConf()
        {
            IConfigurationService dbConf = GetConfiguration();
            if(dbConf.Mail != null) Conf.Mail = dbConf.Mail;
            if (dbConf.Defect != null) Conf.Defect = dbConf.Defect;
            if (dbConf.Issue != null) Conf.Issue = dbConf.Issue;
        }

        public IConfigurationService GetConfiguration()
        {
            using (var service = Service)
            {
                return service.GetConfiguration();
            }

        }

        public bool Store(IIssueConfiguration item)
        {
            bool result = false;
            try
            {
                using (var service = Service)
                {
                    result = service.SaveIssue(item);
                    if (result) Conf.Issue = item;
                }
            }catch(Exception e)
            {
                Console.Write("Error on save item {0}: {1}", item.ServiceName, e.Message);
            }
            
            return result;

        }
        
        public bool Store(IMailConfiguration item)
        {
            bool result = false;
            try
            {
                using (var service = Service)
                {
                    result = service.SaveMail(item);
                    if(result) Conf.Mail = item;
                }
            }
            catch (Exception e)
            {
                Console.Write("Error on save item {0}: {1}", item.ServiceName, e.Message);
            }

            return result;
        }

        public bool Store(IDefectConfiguration item)
        {
            bool result = false;
            try
            {
                using (var service = Service)
                {
                    result = service.SaveDefect(item);
                    if (result) Conf.Defect = item;
                }
            }
            catch (Exception e)
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
