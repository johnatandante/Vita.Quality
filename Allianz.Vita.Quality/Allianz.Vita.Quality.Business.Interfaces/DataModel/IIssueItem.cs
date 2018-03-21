using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;

namespace Allianz.Vita.Quality.Business.Interfaces.DataModel
{
    /// <summary>
    /// Issue Fields
    /// - Id
    /// - Area
    /// - SubArea
    /// - Assegnatario
    /// - Creato
    /// - Data Risoluzione IT
    /// - Date Reopen
    /// - Esamina
    /// - Nome Gruppo Life
    /// - Priorità
    /// - Progetto
    /// - Riepilogo
    /// - Segnalazione Digital Agency
    /// - Stato
    /// - Tipologia issue
    /// 
    /// Not Yet Implemented
    /// - Esamina Richiesta
    /// - Team Accenture
    /// - Team Allianz
    /// - Tipo segnalazione
    /// </summary>
    public interface IIssueItem : IItem
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
