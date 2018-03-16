using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;

namespace Allianz.Vita.Quality.Models
{
    public class IssueViewModel
    {

        IIssueConfiguration Conf
        {
            get
            {
                return ServiceFactory.Get<IConfigurationService>().Issue;

            }
        }

        public IssueViewModel() { }

        public IssueViewModel(IIssueItem issue)
        {
            Id = issue.Id;
            Assignee = issue.Assignee;
            Created = issue.Created;
            ResolvedOn = issue.ResolvedOn;
            Esamina = issue.Esamina;
            NomeGruppoLife = issue.NomeGruppoLife;
            Priority = issue.Priority;
            Project = issue.Project;
            Summary = issue.Summary;
            DigitalAgency = issue.DigitalAgency.HasValue && issue.DigitalAgency.Value;
            Status = issue.Status;
            IssueType = issue.IssueType;

            // Email
            Email = Assignee + "@Allianz.it";
            Url = Conf.IssueSystemUrl + "browse/" + Id;
        }

        public string Id { get; set; }

        public string Area { get; set; }

        public string SubArea { get; set; }

        public string Assignee { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public DateTime Created { get; set; }

        public DateTime? ResolvedOn { get; set; }

        public DateTime? ReopenedOn { get; set; }

        public string Esamina { get; set; }

        public string NomeGruppoLife { get; set; }

        public string Priority { get; set; }

        public string Project { get; set; }

        public string Summary { get; set; }

        public bool DigitalAgency { get; set; }

        public string Status { get; set; }

        public string IssueType { get; set; }

    }
}