using Allianz.Vita.Quality.Business.Interfaces.Service;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class ConfigurationServiceFake : IConfigurationService
    {
        public IMailConfiguration Mail { get; set; }

        public IIssueConfiguration Issue { get; set; }

        public IDefectConfiguration Defect { get; set; }

        class MailConfiguration : IMailConfiguration
        {
            public string MailServiceUrl => string.Empty;

            public string IssueFolderPath => string.Empty;

            public string CompletedFolderPath => string.Empty;

            public string DefaultSender => string.Empty;

            public string ServiceName => string.Empty;

            public string Url => string.Empty;
        }

        class IssueConfiguration : IIssueConfiguration
        {
            public string IssueSystemUrl => string.Empty;

            public int MaxPageItems => 0;

            public string ReopenedFieldName => string.Empty;

            public string NomeGruppoLifeFieldName => string.Empty;

            public string DigitalAgencyFieldName => string.Empty;

            public string WorklogQuery => string.Empty;

            public string ServiceName => string.Empty;

            public string Url => string.Empty;
        }

        class DefectConfiguration : IDefectConfiguration
        {
            public string Iteration => string.Empty;

            public string AreaPath => string.Empty;

            public string SurveySystem => string.Empty;

            public string WebAppId => string.Empty;

            public string Environment => string.Empty;

            public string Severity => string.Empty;

            public string DefectState => string.Empty;

            public string DefectType => string.Empty;

            public string TrackingSystemUrl => string.Empty;

            public string Company => string.Empty;

            public string ProjectPath => string.Empty;

            public string UserAreaPath => string.Empty;

            public string WorkingFeature => string.Empty;

            public string WorkItemType => string.Empty;

            public string ServiceName => string.Empty;

            public string Url => string.Empty;
        }

    }
}
