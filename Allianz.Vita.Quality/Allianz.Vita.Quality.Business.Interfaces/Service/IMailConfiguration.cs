namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IMailConfiguration
    {
        string MailServiceUrl { get; }
        string IssueFolderPath { get; }
        string IssueCompletedFolderPath { get; }
        string DefaultSender { get; }
    }
}
