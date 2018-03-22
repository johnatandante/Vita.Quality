using Allianz.Vita.Quality.Business.Interfaces.Service;
using System.Net;

namespace Allianz.Vita.Quality.Business.Models
{
    public class SettingsData
    {
        public MailConfiguration mail { get; set; }
        public DefectConfiguration defect { get; set; }
        public IssueConfiguration issue { get; set; }

        public void SetData(IMailConfiguration conf, NetworkCredential credential)
        {
            mail = new MailConfiguration(conf);
            mail.credential = new Credential(credential);
        }

        public void SetData(IDefectConfiguration conf, NetworkCredential credential)
        {
            defect = new DefectConfiguration(conf);
            defect.credential = new Credential(credential);
        }
        public void SetData(IIssueConfiguration conf, NetworkCredential credential)
        {
            issue = new IssueConfiguration(conf);
            issue.credential = new Credential(credential); 
        }

        public class Credential
        {
            public Credential() { }
            public Credential(NetworkCredential credential)
            {
                username = credential.UserName;
                domain = credential.Domain;
                password = credential.Password;
            }

            public string username { get; set; }
            public string domain { get; set; }
            public string password { get; set; }
        }

        public class MailConfiguration : BaseConfiguration, IMailConfiguration
        {
            public MailConfiguration() { }
            public MailConfiguration(IMailConfiguration conf)
                : base(conf.ServiceName, conf.Url) {
                IssueFolderPath = conf.IssueFolderPath;
                CompletedFolderPath = conf.CompletedFolderPath;
                DefaultSender = conf.DefaultSender;
            }

            public string IssueFolderPath { get; set; }

            public string CompletedFolderPath { get; set; }

            public string DefaultSender { get; set; }

        }

        public class DefectConfiguration : BaseConfiguration, IDefectConfiguration
        {
            public DefectConfiguration() { }
            public DefectConfiguration(IDefectConfiguration conf)
                : base(conf.ServiceName, conf.Url) {

                Iteration = conf.Iteration;
                AreaPath = conf.AreaPath;
                SurveySystem = conf.SurveySystem;
                WebAppId = conf.WebAppId;
                Environment = conf.Environment;
                Severity = conf.Severity;
                DefectState = conf.DefectState;
                DefectType = conf.DefectType;
                Company = conf.Company;
                ProjectPath = conf.ProjectPath;
                UserAreaPath = conf.UserAreaPath;
                WorkingFeature = conf.WorkingFeature;
                WorkItemType = conf.WorkItemType;
            }

            public string Iteration { get; set; }

            public string AreaPath { get; set; }

            public string SurveySystem { get; set; }

            public string WebAppId { get; set; }

            public string Environment { get; set; }

            public string Severity { get; set; }

            public string DefectState { get; set; }

            public string DefectType { get; set; }

            public string Company { get; set; }

            public string ProjectPath { get; set; }

            public string UserAreaPath { get; set; }

            public string WorkingFeature { get; set; }

            public string WorkItemType { get; set; }

        }

        public class IssueConfiguration : BaseConfiguration, IIssueConfiguration
        {
            public IssueConfiguration() { }
            public IssueConfiguration(IIssueConfiguration conf)
                : base(conf.ServiceName, conf.Url) {

                MaxPageItems = conf.MaxPageItems;
                ReopenedFieldName = conf.ReopenedFieldName;
                NomeGruppoLifeFieldName = conf.NomeGruppoLifeFieldName;
                DigitalAgencyFieldName = conf.DigitalAgencyFieldName;
                WorklogQuery = conf.WorklogQuery;

            }

            public int MaxPageItems { get; set; }

            public string ReopenedFieldName { get; set; }

            public string NomeGruppoLifeFieldName { get; set; }

            public string DigitalAgencyFieldName { get; set; }

            public string WorklogQuery { get; set; }

        }

        public class BaseConfiguration : IConfigurationItem
        {
            public BaseConfiguration() : this(string.Empty, string.Empty) { }
            public BaseConfiguration(string serviceName, string url)
            {
                ServiceName = serviceName;
                Url = url;
            }

            public string ServiceName { get; set; }

            public string Url { get; set; }

            public Credential credential { get; set; }

        }

    }
}
