using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    [Table("IssueConfiguration")]
    public class IssueConfigurationDbModel : IIssueConfiguration
    {
        
        public IssueConfigurationDbModel() { }

        public IssueConfigurationDbModel(IIssueConfiguration item)
        {
            MaxPageItems = item.MaxPageItems;

            ReopenedFieldName = item.ReopenedFieldName;

            NomeGruppoLifeFieldName = item.NomeGruppoLifeFieldName;

            DigitalAgencyFieldName = item.DigitalAgencyFieldName;

            WorklogQuery = item.WorklogQuery;

            ServiceName = item.ServiceName;

            Url = item.Url;

            StartDate = DateTime.Now;

        }

        [ForeignKey("Configuration")]
        public int Id { get; set; }

        public int MaxPageItems { get; set; }

        public string ReopenedFieldName { get; set; }

        public string NomeGruppoLifeFieldName { get; set; }

        public string DigitalAgencyFieldName { get; set; }

        public string WorklogQuery { get; set; }

        public string ServiceName { get; set; }

        public string Url { get; set; }

        public DateTime StartDate { get; set; }

        public int? ConfigurationId { get; set; }

        public virtual ConfigurationDbModel Configuration { get; set; }
    }
}