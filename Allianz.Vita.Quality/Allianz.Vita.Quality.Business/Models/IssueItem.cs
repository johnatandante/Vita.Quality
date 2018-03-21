using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using System;

namespace Allianz.Vita.Quality.Business.Models
{
    public class IssueItem : IIssueItem
    {
        public string Id { get; set; }

        public string Area { get; set; }

        public string SubArea { get; set; }

        public string Assignee { get; set; }

        public DateTime Created { get; set; }

        public DateTime? ResolvedOn { get; set; }

        public DateTime? ReopenedOn { get; set; }

        public string Esamina { get; set; }

        public string NomeGruppoLife { get; set; }

        public string Priority { get; set; }

        public string Project { get; set; }

        public string Summary { get; set; }

        public bool? DigitalAgency { get; set; }

        public string Status { get; set; }

        public string IssueType { get; set; }

        public string Email { get; set; }

    }
}
