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
                var current = Configuration.AppConfiguration
                    .OrderByDescending(t => t.StartDate)
                    .FirstOrDefault();

                return current;
            }
        }

        public IConfigurationService GetConfiguration()
        {
            ConfigurationServiceData result = new ConfigurationServiceData();
            if (Current != null)
            {
                if(Current.MailId.HasValue || Current.Mail != null)
                    result.Mail = new ConfigurationServiceData.MailConfigurationData(Current.Mail);
                if (Current.IssueId.HasValue || Current.Issue != null)
                    result.Issue = new ConfigurationServiceData.IssueConfigurationData(Current.Issue);
                if (Current.DefectId.HasValue || Current.Defect != null)
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
            
            if (Current.Defect == null || (dbItem.StartDate <= DateTime.Now && dbItem.StartDate > Current.Defect.StartDate))
            {
                dbItem.ConfigurationId = Current.ID;
                dbItem.Configuration = Current;
            }

            Configuration.DefectConfiguration.Add(dbItem);

            return Configuration.SaveChanges() > 0;
        }

        public bool SaveMail(IMailConfiguration item)
        {
            MailConfigurationDbModel dbItem = new MailConfigurationDbModel(item);
            
            if (Current.Mail == null || (dbItem.StartDate <= DateTime.Now && dbItem.StartDate > Current.Mail.StartDate))
            {
                dbItem.ConfigurationId = Current.ID;
                dbItem.Configuration = Current;
            }

            Configuration.MailConfiguration.Add(dbItem);
            
            return Configuration.SaveChanges() > 0;
        }
        
        public bool SaveIssue(IIssueConfiguration item)
        {
            IssueConfigurationDbModel dbItem = new IssueConfigurationDbModel(item);
                        
            if (Current.Issue == null || (dbItem.StartDate <= DateTime.Now && dbItem.StartDate > Current.Issue.StartDate))
            {
                dbItem.ConfigurationId = Current.ID;
                dbItem.Configuration = Current;
            }

            Configuration.IssueConfiguration.Add(dbItem);

            return Configuration.SaveChanges() > 0;
        }
        
        ConfigurationDbModel[] GetConfigurations()
        {
            return Configuration.AppConfiguration.ToArray();
        }

        public void StoreConfigurations(IMailConfiguration mailConf = null, IDefectConfiguration defectConf = null, IIssueConfiguration issueConf = null)
        {

            ConfigurationDbModel newConf = new ConfigurationDbModel();

            UpdateConfiguration(newConf, mailConf, defectConf, issueConf);
            Configuration.AppConfiguration.Add(newConf);

            Configuration.SaveChanges();

        }

        private bool UpdateConfiguration(ConfigurationDbModel newConf, IMailConfiguration mailConf = null, IDefectConfiguration defectConf = null, IIssueConfiguration issueConf = null)
        {
            bool hasChanged = false;

            if (issueConf != null)
            {
                IssueConfigurationDbModel issueNewConf = new IssueConfigurationDbModel(issueConf);
                Configuration.IssueConfiguration.Add(issueNewConf);
                newConf.IssueId = issueNewConf.Id;
                newConf.Issue = issueNewConf;
                hasChanged = true;
            }

            if (defectConf != null)
            {
                DefectConfigurationDbModel defectNewConf = new DefectConfigurationDbModel(defectConf);
                Configuration.DefectConfiguration.Add(defectNewConf);
                newConf.IssueId = defectNewConf.Id;
                newConf.Defect = defectNewConf;
                hasChanged = true;
            }

            if (issueConf != null)
            {
                MailConfigurationDbModel mailNewConf = new MailConfigurationDbModel(mailConf);
                Configuration.MailConfiguration.Add(mailNewConf);
                newConf.IssueId = mailNewConf.Id;
                newConf.Mail = mailNewConf;
                hasChanged = true;
            }

            return hasChanged;
        }

        public void Dispose()
        {
            Configuration.Dispose();
        }
    }
}
