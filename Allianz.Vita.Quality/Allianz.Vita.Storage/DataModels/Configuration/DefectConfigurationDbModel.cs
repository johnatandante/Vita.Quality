using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    public class DefectConfigurationDbModel : IDefectConfiguration
    {
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

        public DateTime StartDate { get; set; }
    }
}