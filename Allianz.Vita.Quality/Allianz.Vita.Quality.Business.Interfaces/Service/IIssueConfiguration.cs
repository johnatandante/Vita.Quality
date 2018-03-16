namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IIssueConfiguration
    {
        string IssueSystemUrl { get; }
        int MaxPageItems { get; }
        string ReopenedFieldName { get; }
        string NomeGruppoLifeFieldName { get; }
        string DigitalAgencyFieldName { get; }
        string WorklogQuery { get; }
    }
}
