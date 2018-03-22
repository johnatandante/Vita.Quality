namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IConfigurationService : IService
    {
        IMailConfiguration Mail { get; set; }
        IIssueConfiguration Issue { get; set; }
        IDefectConfiguration Defect { get; set; }
        
    }
    
}
