namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IMailConfiguration : IConfigurationItem
    {
        string IssueFolderPath { get; }
        string CompletedFolderPath { get; }
        string DefaultSender { get; }
    }
}
