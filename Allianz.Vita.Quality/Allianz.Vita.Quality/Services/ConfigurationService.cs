using Allianz.Vita.Quality.Business.Interfaces.Service;

namespace Allianz.Vita.Quality.Services
{
    public class ConfigurationService : IConfigurationService
    {
        
        public IMailConfiguration Mail { get; }

        public IIssueConfiguration Issue { get; }

        public IDefectConfiguration Defect { get; }

        public ConfigurationService()
        {
            Mail = new MailConfigurationItem();
            Defect = new DefectConfigurationItem();
            Issue = new IssueConfigurationItem();
        }
    }

}
