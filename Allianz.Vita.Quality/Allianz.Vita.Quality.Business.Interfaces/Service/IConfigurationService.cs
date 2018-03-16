namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IConfigurationService : IService
    {
        IMailConfiguration Mail { get; }
        IIssueConfiguration Issue { get; }
        IDefectConfiguration Defect { get; }
    }
    
}
