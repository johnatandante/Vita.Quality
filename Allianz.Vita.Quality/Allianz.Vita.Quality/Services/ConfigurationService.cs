using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Web.Configuration;

namespace Allianz.Vita.Quality.Services
{
    public class ConfigurationService : IConfigurationService
    {
        
        public IMailConfiguration Mail { get; set; }

        public IIssueConfiguration Issue { get; set; }

        public IDefectConfiguration Defect { get; set; }

        public ConfigurationService()
        {
            Mail = new WebConfigMailConfigurationItem();
            Defect = new WebConfigDefectConfigurationItem();
            Issue = new WebConfigIssueConfigurationItem();
        }

        class WebConfigDefectConfigurationItem : IDefectConfiguration
        {

            public string ServiceName => "Defect";

            public string Iteration
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultIteration"].ToString();
                }
            }

            public string AreaPath
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultAreaPath"].ToString();
                }
            }

            public string SurveySystem
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultSurveySystem"].ToString();
                }
            }

            public string WebAppId
            {
                get
                {
                    return WebConfigurationManager.AppSettings["CurrentWebAppId"].ToString();
                }
            }

            public string Environment
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultEnvironment"].ToString();
                }
            }

            public string Severity
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultSeverity"].ToString();
                }
            }

            public string DefectState
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultDefectState"].ToString();
                }
            }

            public string DefectType
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultDefectType"].ToString();
                }
            }

            public string Url
            {
                get
                {
                    return WebConfigurationManager.AppSettings["TrackingSystemUrl"].ToString();
                }
            }

            public string ProjectPath
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultProjectPath"].ToString();
                }
            }

            public string WorkItemType
            {
                get
                {
                    return WebConfigurationManager.AppSettings["DefaultDefectWorkItemType"].ToString();
                }
            }

            public string UserAreaPath
            {
                get
                {
                    return WebConfigurationManager.AppSettings["TrackingSystemUserAreaPath"].ToString();
                }
            }

            public string WorkingFeature
            {
                get
                {
                    return WebConfigurationManager.AppSettings["TrackingSystemWorkingFeature"].ToString();
                }
            }

            public string Company
            {
                get
                {
                    return WebConfigurationManager.AppSettings["TrackingSystemCompany"].ToString();
                }
            }

        }

        class WebConfigIssueConfigurationItem : IIssueConfiguration
        {
            public string ServiceName => "Issue";

            public string Url => WebConfigurationManager.AppSettings["IssueSystemUrl"].ToString();

            public int MaxPageItems => Convert.ToInt32(WebConfigurationManager.AppSettings["IssueMaxPageItems"]);

            public string ReopenedFieldName => WebConfigurationManager.AppSettings["ReopenedFieldName"].ToString();

            public string NomeGruppoLifeFieldName => WebConfigurationManager.AppSettings["NomeGruppoLifeFieldName"].ToString();

            public string DigitalAgencyFieldName => WebConfigurationManager.AppSettings["DigitalAgencyFieldName"].ToString();

            public string WorklogQuery => WebConfigurationManager.AppSettings["WorklogQuery"].ToString();
        }

        class WebConfigMailConfigurationItem : IMailConfiguration
        {
            public string Url => WebConfigurationManager.AppSettings["MailServiceUrl"].ToString();

            public string IssueFolderPath => WebConfigurationManager.AppSettings["MailIssueFolderPath"].ToString();

            public string DefaultSender => WebConfigurationManager.AppSettings["MailDefaultSender"].ToString();

            public string CompletedFolderPath => WebConfigurationManager.AppSettings["MailIssueCompletedFolderPath"].ToString();

            public string ServiceName => "Mail";
        }

    }

}
