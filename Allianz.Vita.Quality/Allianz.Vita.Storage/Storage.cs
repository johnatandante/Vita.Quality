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


        public Storage(NetworkCredential credential=null, Uri uri=null)
        {
            Configuration = new ConfigurationDbContext();

        }

        public ConfigurationDbModel[] GetConfiguration()
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
