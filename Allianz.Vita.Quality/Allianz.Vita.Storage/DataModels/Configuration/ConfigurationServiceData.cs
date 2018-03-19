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
                IssueCompletedFolderPath = model.IssueCompletedFolderPath;
                DefaultSender = model.DefaultSender;
                ServiceName = model.ServiceName;
                Url = model.Url;
            }

            public string IssueFolderPath { get; set; }

            public string IssueCompletedFolderPath { get; set; }

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
                DefaultIteration = model.DefaultIteration;
                DefaultAreaPath = model.DefaultAreaPath;
                DefaultSurveySystem = model.DefaultSurveySystem;
                CurrentWebAppId = model.CurrentWebAppId;
                DefaultEnvironment = model.DefaultEnvironment;
                DefaultSeverity = model.DefaultSeverity;
                DefaultDefectState = model.DefaultDefectState;
                DefaultDefectType = model.DefaultDefectType;
                TrackingSystemCompany = model.TrackingSystemCompany;
                DefaultProjectPath = model.DefaultProjectPath;
                TrackingSystemUserAreaPath = model.TrackingSystemUserAreaPath;
                TrackingSystemWorkingFeature = model.TrackingSystemWorkingFeature;
                DefaultDefectWorkItemType = model.DefaultDefectWorkItemType;
                ServiceName = model.ServiceName;
                Url = model.Url;
            }

            public string DefaultIteration { get; set; }

            public string DefaultAreaPath { get; set; }

            public string DefaultSurveySystem { get; set; }

            public string CurrentWebAppId { get; set; }

            public string DefaultEnvironment { get; set; }

            public string DefaultSeverity { get; set; }

            public string DefaultDefectState { get; set; }

            public string DefaultDefectType { get; set; }

            public string TrackingSystemCompany { get; set; }

            public string DefaultProjectPath { get; set; }

            public string TrackingSystemUserAreaPath { get; set; }

            public string TrackingSystemWorkingFeature { get; set; }

            public string DefaultDefectWorkItemType { get; set; }

            public string ServiceName { get; set; }

            public string Url { get; set; }
        }
    }
}
