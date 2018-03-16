using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Web.Configuration;

namespace Allianz.Vita.Quality.Services
{
    class IssueConfigurationItem : IIssueConfiguration
    {
        public string IssueSystemUrl =>  WebConfigurationManager.AppSettings["IssueSystemUrl"].ToString();

        public int MaxPageItems => Convert.ToInt32(WebConfigurationManager.AppSettings["IssueMaxPageItems"]);

        public string ReopenedFieldName => WebConfigurationManager.AppSettings["ReopenedFieldName"].ToString();

        public string NomeGruppoLifeFieldName => WebConfigurationManager.AppSettings["NomeGruppoLifeFieldName"].ToString();

        public string DigitalAgencyFieldName => WebConfigurationManager.AppSettings["DigitalAgencyFieldName"].ToString();

        public string WorklogQuery =>  WebConfigurationManager.AppSettings["WorklogQuery"].ToString();
    }
}