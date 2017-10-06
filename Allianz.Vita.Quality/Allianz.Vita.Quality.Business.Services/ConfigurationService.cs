﻿using Allianz.Vita.Quality.Business.Interfaces;
using System.Web.Configuration;

namespace Allianz.Vita.Quality.Business.Services
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

        public string Password
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailHashedPassword"].ToString();
            }
        }

        public string AccountName
        {
            get
            {
                return WebConfigurationManager.AppSettings["MailAccountName"].ToString();
            }
        }

        public string TrackingSystemCompany
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemCompany"].ToString();
            }
        }

        public string TrackingSystemAccountName
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemAccountName"].ToString();
            }
        }

        public string TrackingSystemDomainName
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemDomainName"].ToString();
            }
        }

        public string TrackingSystemHashedPassword
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemHashedPassword"].ToString();
            }
        }

        public string DefaultDefectWorkItemType
        {
            get
            {
                return WebConfigurationManager.AppSettings["DefaultDefectWorkItemType"].ToString();
            }
        }
    }
}