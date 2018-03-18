using Allianz.Vita.Quality.Business.Interfaces.Service;
using System.Web.Configuration;

namespace Allianz.Vita.Quality.Services
{
    class MailConfigurationItem : IMailConfiguration
    {
        public string Url => WebConfigurationManager.AppSettings["MailServiceUrl"].ToString();
        
        public string IssueFolderPath => WebConfigurationManager.AppSettings["MailIssueFolderPath"].ToString();

        public string DefaultSender => WebConfigurationManager.AppSettings["MailDefaultSender"].ToString();

        public string IssueCompletedFolderPath => WebConfigurationManager.AppSettings["MailIssueCompletedFolderPath"].ToString();

        public string ServiceName => "Mail";
    }
}