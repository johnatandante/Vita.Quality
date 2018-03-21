using System;

namespace Allianz.Vita.Quality.Business.Interfaces.DataModel
{
    /// <summary>
    /// KPI Info
    /// - Assignee
    /// - Consumed
    /// - RemainingTime
    /// - DueDate
    /// - StartDate
    /// - EndDate
    /// - Closed
    /// - Delayed
    /// </summary>
    public interface IKpiItem
    {
        string Assignee { get; }
        DateTime Consumed { get; }
        DateTime RemainingTime { get; }
        DateTime DueDate { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        bool Closed { get; }
        bool Delayed { get; }
    }
}
