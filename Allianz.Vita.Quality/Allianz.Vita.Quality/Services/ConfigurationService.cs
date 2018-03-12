using Allianz.Vita.Quality.Business.Interfaces.Service;
using System.Web.Configuration;

namespace Allianz.Vita.Quality.Services
{
    public class ConfigurationService : IConfigurationService
    {
             
        
        public string DefaultIteration
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultIteration"].ToString();
            }
        }

        public string DefaultAreaPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultAreaPath"].ToString();
            }
        }

        public string DefaultSurveySystem
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultSurveySystem"].ToString();
            }
        }

        public string CurrentWebAppId
        {
            get
            {
                return WebConfigurationManager.AppSettings["CurrentWebAppId"].ToString();
            }
        }

        public string DefaultEnvironment
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultEnvironment"].ToString();
            }
        }

        public string DefaultSeverity
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultSeverity"].ToString();
            }
        }

        public string DefaultDefectState
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultDefectState"].ToString();
            }
        }

        public string DefaultDefectType
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultDefectType"].ToString();
            }
        }

        public string TrackingSystemUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemUrl"].ToString();
            }
        }

        public string DefaultProjectPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultProjectPath"].ToString();
            }
        }

        public string MailServiceUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailServiceUrl"].ToString();
            }
        }
        
        public string TrackingSystemCompany
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemCompany"].ToString();
            }
        }
        
        public string DefaultDefectWorkItemType
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultDefectWorkItemType"].ToString();
            }
        }

        public string IssueFolderPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailIssueFolderPath"].ToString();
            }

        }

        public string DefaultSender
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailDefaultSender"].ToString();
            }

        }

        public string TrackingSystemUserAreaPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemUserAreaPath"].ToString();
            }
        }

        public string TrackingSystemWorkingFeature
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemWorkingFeature"].ToString();
            }
        }

        public string IssueCompletedFolderPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailIssueCompletedFolderPath"].ToString();                
            }
        }

    }
}
