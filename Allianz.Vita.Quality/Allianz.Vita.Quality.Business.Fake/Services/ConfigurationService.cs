using Allianz.Vita.Quality.Business.Interfaces;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class ConfigurationService : IConfigurationService
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

        public string DefaultProjectPath => string.Empty;

        public string MailServiceUrl => string.Empty;
        
        public string TrackingSystemCompany => string.Empty;

        public string DefaultDefectWorkItemType => string.Empty;

        public string IssueFolderPath => string.Empty;

        public string DefaultSender => string.Empty;

        public string TrackingSystemUserAreaPath => string.Empty;

        public string TrackingSystemWorkingFeature => string.Empty;

        public string IssueCompletedFolderPath => string.Empty;
    }
}
