namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IDefectConfiguration : IConfigurationItem
    {
        string DefaultIteration { get; }
        string DefaultAreaPath { get; }
        string DefaultSurveySystem { get; }
        string CurrentWebAppId { get; }
        string DefaultEnvironment { get; }
        string DefaultSeverity { get; }
        string DefaultDefectState { get; }
        string DefaultDefectType { get; }
        string TrackingSystemCompany { get; }
        string DefaultProjectPath { get; }
        string TrackingSystemUserAreaPath { get; }
        string TrackingSystemWorkingFeature { get; }
        string DefaultDefectWorkItemType { get; }
    }
}
