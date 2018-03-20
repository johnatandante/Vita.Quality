namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IDefectConfiguration : IConfigurationItem
    {
        string Iteration { get; }
        string AreaPath { get; }
        string SurveySystem { get; }
        string WebAppId { get; }
        string Environment { get; }
        string Severity { get; }
        string DefectState { get; }
        string DefectType { get; }
        string Company { get; }
        string ProjectPath { get; }
        string UserAreaPath { get; }
        string WorkingFeature { get; }
        string WorkItemType { get; }
    }
}
