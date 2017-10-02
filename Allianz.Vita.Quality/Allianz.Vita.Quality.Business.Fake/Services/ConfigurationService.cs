using Allianz.Vita.Quality.Business.Interfaces;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string DefaultIteration
        {
            get
            {
                return "Vita\\Dev\\SUV\\";
            }
        }

        public string DefaultAreaPath
        {
            get
            {
                return "Vita\\SUV";
            }
        }

        public string DefaultSurveySystem
        {
            get
            {
                return "SRM";
            }
        }

        public string CurrentWebAppId
        {
            get
            {
                return "SUV XXX";
            }
        }

        public string DefaultEnvironment
        {
            get
            {
                return "Test";
            }
        }

        public string DefaultSeverity
        {
            get
            {
                return "Low";
            }
        }

        public string DefaultDefectState
        {
            get
            {
                return "New";
            }
        }

        public string DefaultDefectType
        {
            get
            {
                return "Altro";
            }
        }
    }
}
