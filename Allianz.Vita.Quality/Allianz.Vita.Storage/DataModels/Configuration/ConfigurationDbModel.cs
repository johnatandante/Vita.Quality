using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    [Table("Configuration")]
    public class ConfigurationDbModel
    {
        [Key]
        public int ID { get; set; }

        public DateTime StartDate { get; set; }

        public MailConfigurationDbModel Mail { get; set; }

        public IssueConfigurationDbModel Issue { get; set; }

        public DefectConfigurationDbModel Defect { get; set; }

    }

}
