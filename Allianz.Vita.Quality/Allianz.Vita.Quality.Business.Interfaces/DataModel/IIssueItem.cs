using System;

namespace Allianz.Vita.Quality.Business.Interfaces.DataModel
{
    public interface IIssueItem
    {
        string Id { get; }
        string Area { get; }
        string SubArea { get; }
        string Assignee { get; }        
        DateTime Created { get; }
        DateTime? ResolvedOn { get; }
        DateTime? ReopenedOn { get; }
        string Esamina { get; }
        string NomeGruppoLife { get; }
        string Priority { get; }
        string Project { get; }
        string Summary { get; }
        bool? DigitalAgency { get; }
        string Status { get; }
        string IssueType { get; }
        string Email { get; }
    }
}
