using Allianz.Vita.Quality.Business.Interfaces;
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
    }
}
