using Allianz.Vita.Quality.Business.Interfaces.Service;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class ConfigurationServiceFake : IConfigurationService
    {
        public IMailConfiguration Mail { get; }

        public IIssueConfiguration Issue { get; }

        public IDefectConfiguration Defect { get; }

        class MailConfiguration : IMailConfiguration
        {
            public string MailServiceUrl => string.Empty;

            public string IssueFolderPath => string.Empty;

            public string IssueCompletedFolderPath => string.Empty;

            public string DefaultSender => string.Empty;
        }

        class IssueConfiguration : IIssueConfiguration
        {
            public string IssueSystemUrl => string.Empty;

            public int MaxPageItems => 0;

            public string ReopenedFieldName => string.Empty;

            public string NomeGruppoLifeFieldName => string.Empty;

            public string DigitalAgencyFieldName => string.Empty;

            public string WorklogQuery => string.Empty;
        }

        class DefectConfiguration : IDefectConfiguration
        {
            public string DefaultIteration => string.Empty;

            public string DefaultAreaPath => string.Empty;

            public string DefaultSurveySystem => string.Empty;

            public string CurrentWebAppId => string.Empty;

            public string DefaultEnvironment => string.Empty;

            public string DefaultSeverity => string.Empty;

            public string DefaultDefectState => string.Empty;

            public string DefaultDefectType => string.Empty;

            public string TrackingSystemUrl => string.Empty;

            public string TrackingSystemCompany => string.Empty;

            public string DefaultProjectPath => string.Empty;

            public string TrackingSystemUserAreaPath => string.Empty;

            public string TrackingSystemWorkingFeature => string.Empty;

            public string DefaultDefectWorkItemType => string.Empty;
        }

    }
}
