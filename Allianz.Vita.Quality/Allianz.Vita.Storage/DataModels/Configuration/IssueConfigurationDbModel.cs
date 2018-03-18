using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    public class IssueConfigurationDbModel : IIssueConfiguration
    {
        public int MaxPageItems { get; set; }

        public string ReopenedFieldName { get; set; }

        public string NomeGruppoLifeFieldName { get; set; }

        public string DigitalAgencyFieldName { get; set; }

        public string WorklogQuery { get; set; }

        public string ServiceName { get; set; }

        public string Url { get; set; }

        public DateTime StartDate { get; set; }
    }
}