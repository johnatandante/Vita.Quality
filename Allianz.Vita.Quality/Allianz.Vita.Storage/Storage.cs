using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Storage.DataContext;
using Allianz.Vita.Storage.DataModels.Configuration;
using System;
using System.Linq;
using System.Net;

namespace Allianz.Vita.Storage
{
    public class Storage : IDisposable
    {

        static Storage()
        {
            typeof(System.Data.Entity.SqlServer.SqlProviderServices).ToString();
        }

        public NetworkCredential Credentials { get; set; }

        //public Uri Uri { get; set; }

        ConfigurationDbContext Configuration;

        ConfigurationDbModel Current
        {
            get
            {
                return Configuration.AppConfiguration
                    .OrderByDescending(t => t.StartDate)
                    .FirstOrDefault();
            }
        }

        public IConfigurationService GetConfiguration()
        {
            ConfigurationServiceData result = new ConfigurationServiceData();
            if (Current != null)
            {
                if(Current.Mail != null)
                    result.Mail = new ConfigurationServiceData.MailConfigurationData(Current.Mail);
                if (Current.Issue != null)
                    result.Issue = new ConfigurationServiceData.IssueConfigurationData(Current.Issue);
                if (Current.Defect != null)
                    result.Defect = new ConfigurationServiceData.DefectConfigurationData(Current.Defect);
            }

            return result;

        }

        public Storage(NetworkCredential credential=null, Uri uri=null)
        {
            Configuration = new ConfigurationDbContext();

        }

        private static void Trace(IConfigurationItem item)
        {
            if (item == null) return;
            Console.WriteLine(item.ServiceName + ": " + item.Url);
        }

        public bool SaveDefect(IDefectConfiguration item)
        {
            DefectConfigurationDbModel dbItem = new DefectConfigurationDbModel(item);

            Configuration.DefectConfiguration.Add(dbItem);
            int result = Configuration.SaveChanges();

            if (Current.Defect == null || (dbItem.StartDate <= DateTime.Now && dbItem.StartDate > Current.Defect.StartDate))
            {
                dbItem.Configuration = Current;
                result += Configuration.SaveChanges();
            }

            return result > 0;
        }

        public bool SaveMail(IMailConfiguration item)
        {
            MailConfigurationDbModel dbItem = new MailConfigurationDbModel(item);
            Current.Mail = new MailConfigurationDbModel(item);

            Configuration.MailConfiguration.Add(dbItem);
            int result = Configuration.SaveChanges();

            if (Current.Mail == null || (dbItem.StartDate <= DateTime.Now && dbItem.StartDate > Current.Mail.StartDate))
            {
                dbItem.Configuration = Current;
                result += Configuration.SaveChanges();
            }

            return result > 0;
        }

        public bool SaveIssue(IIssueConfiguration item)
        {
            IssueConfigurationDbModel dbItem = new IssueConfigurationDbModel(item);

            Configuration.IssueConfiguration.Add(dbItem);
            int result = Configuration.SaveChanges();
            
            if (Current.Issue == null || (dbItem.StartDate <= DateTime.Now && dbItem.StartDate > Current.Issue.StartDate))
            {
                dbItem.Configuration = Current;
                result += Configuration.SaveChanges();
            }

            return result > 0;
        }
        
        ConfigurationDbModel[] GetConfigurations()
        {
            return Configuration.AppConfiguration.ToArray();
        }

        public void StoreConfiguration(IMailConfiguration mailConf = null, IDefectConfiguration defectConf = null, IIssueConfiguration issueConf = null)
        {
 
            if(issueConf != null)
            {
                IssueConfigurationDbModel issueNewConf = new IssueConfigurationDbModel() {
                    StartDate = DateTime.Now
                };
                Configuration.IssueConfiguration.Add(issueNewConf);
                Current.Issue = issueNewConf;
            }

            if (defectConf != null)
            {
                DefectConfigurationDbModel defectNewConf = new DefectConfigurationDbModel()
                {
                    StartDate = DateTime.Now
                };
                Configuration.DefectConfiguration.Add(defectNewConf);
                Current.Defect = defectNewConf;
            }

            if (issueConf != null)
            {
                MailConfigurationDbModel mailNewConf = new MailConfigurationDbModel()
                {
                    StartDate = DateTime.Now
                };
                Configuration.MailConfiguration.Add(mailNewConf);
                Current.Mail = mailNewConf;
            }

            Configuration.AppConfiguration.Add(Current);
            Configuration.SaveChanges();

        }

        public void Dispose()
        {
            Configuration.Dispose();
        }
    }
}
