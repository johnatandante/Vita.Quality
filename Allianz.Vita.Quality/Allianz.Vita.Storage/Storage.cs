using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Storage.DataContext;
using Allianz.Vita.Storage.DataModels.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Storage
{
    public class Storage : IDisposable
    {

        static Storage()
        {
            typeof(System.Data.Entity.SqlServer.SqlProviderServices).ToString();
        }

        //public NetworkCredential Credentials { get; set; }

        //public Uri Uri { get; set; }

        ConfigurationDbContext Configuration;

        ConfigurationDbModel Current
        {
            get
            {
                return Configuration.AppConfiguration
                    .Where(t => t.StartDate <= DateTime.Now)
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

            foreach(var conf in GetConfigurations()) {
                Console.WriteLine("Conf: " + conf.ID + " - " + conf.StartDate);
                if (conf.Mail != null)
                {
                    Console.WriteLine("Mail: " + conf.Mail.Url + " - " + conf.Mail.StartDate);
                }
                if (conf.Defect != null)
                {
                    Console.WriteLine("Defect: " + conf.Defect.Url + " - " + conf.Defect.StartDate);
                }
                if (conf.Issue != null)
                {
                    Console.WriteLine("Issue: " + conf.Issue.Url + " - " + conf.Issue.StartDate);
                }

            }

        }

        public bool SaveIssue(IIssueConfiguration item)
        {
            IssueConfigurationDbModel dbItem = new IssueConfigurationDbModel(item);

            Configuration.IssueConfiguration.Add(dbItem);
            int result = Configuration.SaveChanges();

            if (Current != null)
            {
                if (Current.Issue == null || (dbItem.StartDate <= DateTime.Now && dbItem.StartDate > Current.Issue.StartDate))
                {
                    Current.Issue = dbItem;
                    result += Configuration.SaveChanges();
                }
            }

            return result > 0;
        }
        
        ConfigurationDbModel[] GetConfigurations()
        {
            return Configuration.AppConfiguration.ToArray();
        }

        public void StoreConfiguration(IMailConfiguration mailConf = null, IDefectConfiguration defectConf = null, IIssueConfiguration issueConf = null)
        {
            ConfigurationDbModel current = Configuration.AppConfiguration
                .Where(t => t.StartDate <= DateTime.Now)
                .OrderByDescending(t => t.StartDate)
                .FirstOrDefault();

            if(issueConf != null)
            {
                IssueConfigurationDbModel issueNewConf = new IssueConfigurationDbModel() {
                    StartDate = DateTime.Now
                };
                Configuration.IssueConfiguration.Add(issueNewConf);
                current.Issue = issueNewConf;
            }

            if (defectConf != null)
            {
                DefectConfigurationDbModel defectNewConf = new DefectConfigurationDbModel()
                {
                    StartDate = DateTime.Now
                };
                Configuration.DefectConfiguration.Add(defectNewConf);
                current.Defect = defectNewConf;
            }

            if (issueConf != null)
            {
                MailConfigurationDbModel mailNewConf = new MailConfigurationDbModel()
                {
                    StartDate = DateTime.Now
                };
                Configuration.MailConfiguration.Add(mailNewConf);
                current.Mail = mailNewConf;
            }

            Configuration.AppConfiguration.Add(current);
        }

        public void Dispose()
        {
            Configuration.Dispose();
        }
    }
}
