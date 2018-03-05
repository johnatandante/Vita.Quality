using Allianz.Vita.Quality.Business.Interfaces;
using System.Web.Configuration;
using System.Runtime.Caching;
using System;

namespace Allianz.Vita.Quality.Services
{
    public class ConfigurationService : IConfigurationService
    {
        
        //const string mailAccountName = "MailAccountName";
        //const string mailHashedPassword = "MailHashedPassword";

        //const string trackingSystemAccountName = "TrackingSystemAccountName";
        //const string trackingSystemDomainName = "TrackingSystemDomainName";
        //const string trackingSystemHashedPassword = "TrackingSystemHashedPassword";        

        //static ObjectCache cache;

        //static CacheItemPolicy policy
        //{
        //    get
        //    {
        //        var p = new CacheItemPolicy
        //        {
        //            AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(120.0)
        //        };
        //        return p;
        //    }
        //}

        //static ConfigurationService()
        //{
        //    cache = MemoryCache.Default;            

        //    cache.Add(mailAccountName, string.Empty, policy);
        //    cache.Add(mailHashedPassword, string.Empty, policy);

        //    cache.Add(trackingSystemAccountName, string.Empty, policy);
        //    cache.Add(trackingSystemDomainName, string.Empty, policy);
        //    cache.Add(trackingSystemHashedPassword, string.Empty, policy);

        //}
        
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

        //public string Password
        //{
        //    get
        //    {
        //        return cache.GetCacheItem(mailHashedPassword).Value as string;
        //        //return WebConfigurationManager.AppSettings["MailHashedPassword"].ToString();
        //    }
        //}

        //public string AccountName
        //{
        //    get
        //    {
        //        return cache.GetCacheItem(mailAccountName).Value as string;
        //        //return WebConfigurationManager.AppSettings["MailAccountName"].ToString();
        //    }
        //}

        public string TrackingSystemCompany
        {
            get
            {
                return WebConfigurationManager.AppSettings["TrackingSystemCompany"].ToString();
            }
        }

        //public string TrackingSystemAccountName
        //{
        //    get
        //    {
        //        return cache.GetCacheItem(trackingSystemAccountName).Value as string;
        //        //return WebConfigurationManager.AppSettings["TrackingSystemAccountName"].ToString();
        //    }
        //}

        //public string TrackingSystemDomainName
        //{
        //    get
        //    {
        //        return cache.GetCacheItem(trackingSystemDomainName).Value as string;
        //        //return WebConfigurationManager.AppSettings["TrackingSystemDomainName"].ToString();
        //    }
        //}

        //public string TrackingSystemHashedPassword
        //{
        //    get
        //    {
        //        return cache.GetCacheItem(trackingSystemHashedPassword).Value as string;
        //        //return WebConfigurationManager.AppSettings["TrackingSystemHashedPassword"].ToString();
        //    }
        //}

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
