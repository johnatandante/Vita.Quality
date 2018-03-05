using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IConfigurationService : IService
    {
        string DefaultIteration { get; }

        string DefaultAreaPath { get; }

        string DefaultSurveySystem { get; }

        string CurrentWebAppId { get; }

        string DefaultEnvironment { get; }

        string DefaultSeverity { get; }

        string DefaultDefectState { get; }

        string DefaultDefectType { get; }

        string TrackingSystemUrl { get; }

        string TrackingSystemCompany { get; }

        string DefaultProjectPath { get; }
        
        string MailServiceUrl { get; }
        
        string DefaultDefectWorkItemType { get; }

        string IssueFolderPath { get; }

        string IssueCompletedFolderPath { get; }

        string DefaultSender { get; }

        string TrackingSystemUserAreaPath { get; }

        string TrackingSystemWorkingFeature { get; }

    }
}
