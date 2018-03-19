using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    public class MailConfigurationDbModel : IMailConfiguration
    {
        public int ID { get; set; }

        public string IssueFolderPath { get; set; }

        public string IssueCompletedFolderPath { get; set; }

        public string DefaultSender { get; set; }

        public string ServiceName { get; set; }

        public string Url { get; set; }

        public DateTime StartDate { get; set; }

    }
}