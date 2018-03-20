using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    [Table("MailConfiguration")]
    public class MailConfigurationDbModel : IMailConfiguration
    {
        public MailConfigurationDbModel() { }

        public MailConfigurationDbModel(IMailConfiguration item)
        {
            CompletedFolderPath = item.CompletedFolderPath;
            IssueFolderPath = item.IssueFolderPath;
            DefaultSender = item.DefaultSender;
            Url = item.Url;
            ServiceName = item.ServiceName;
            StartDate = DateTime.Now;
        }

        [ForeignKey("Configuration")]
        public int Id { get; set; }

        public string IssueFolderPath { get; set; }

        public string CompletedFolderPath { get; set; }

        public string DefaultSender { get; set; }

        public string ServiceName { get; set; }

        public string Url { get; set; }

        public DateTime StartDate { get; set; }

        public virtual ConfigurationDbModel Configuration { get; set; }

    }
}