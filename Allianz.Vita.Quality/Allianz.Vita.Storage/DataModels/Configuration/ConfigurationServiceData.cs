using Allianz.Vita.Quality.Business.Interfaces.Service;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    public class ConfigurationServiceData : IConfigurationService
    {
        public ConfigurationServiceData()
        {
            Mail = new MailConfigurationData();
            Issue = new IssueConfigurationData();
            Defect = new DefectConfigurationData();
        }


        public IMailConfiguration Mail { get; set; }

        public IIssueConfiguration Issue { get; set; }

        public IDefectConfiguration Defect { get; set; }

        public class MailConfigurationData : IMailConfiguration
        {
            public MailConfigurationData() { }
            public MailConfigurationData(MailConfigurationDbModel model)
            {
                IssueFolderPath = model.IssueFolderPath;
                CompletedFolderPath = model.CompletedFolderPath;
                DefaultSender = model.DefaultSender;
                ServiceName = model.ServiceName;
                Url = model.Url;
            }

            public string IssueFolderPath { get; set; }

            public string CompletedFolderPath { get; set; }

            public string DefaultSender { get; set; }

            public string ServiceName { get; set; }

            public string Url { get; set; }
        }

        public class IssueConfigurationData : IIssueConfiguration
        {
            public IssueConfigurationData() { }
            public IssueConfigurationData(IssueConfigurationDbModel model)
            {
                MaxPageItems = model.MaxPageItems;
                ReopenedFieldName = model.ReopenedFieldName;
                NomeGruppoLifeFieldName = model.NomeGruppoLifeFieldName;
                DigitalAgencyFieldName = model.DigitalAgencyFieldName;
                WorklogQuery = model.WorklogQuery;
                ServiceName = model.ServiceName;
                Url = model.Url;

            }

            public int MaxPageItems { get; set; }

            public string ReopenedFieldName { get; set; }

            public string NomeGruppoLifeFieldName { get; set; }

            public string DigitalAgencyFieldName { get; set; }

            public string WorklogQuery { get; set; }

            public string ServiceName { get; set; }

            public string Url { get; set; }
        }

        public class DefectConfigurationData : IDefectConfiguration
        {
            public DefectConfigurationData() { }
            public DefectConfigurationData(DefectConfigurationDbModel model)
            {
                Iteration = model.Iteration;
                AreaPath = model.AreaPath;
                SurveySystem = model.SurveySystem;
                WebAppId = model.WebAppId;
                Environment = model.Environment;
                Severity = model.Severity;
                DefectState = model.DefectState;
                DefectType = model.DefectType;
                Company = model.Company;
                ProjectPath = model.ProjectPath;
                UserAreaPath = model.UserAreaPath;
                WorkingFeature = model.WorkingFeature;
                WorkItemType = model.WorkItemType;
                ServiceName = model.ServiceName;
                Url = model.Url;
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

            public string ServiceName { get; set; }

            public string Url { get; set; }
        }
    }
}
