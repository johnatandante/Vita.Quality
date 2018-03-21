using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    [Table("Configuration")]
    public class ConfigurationDbModel
    {
        public ConfigurationDbModel()
        {
            StartDate = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        public DateTime StartDate { get; set; }
        
        public int? MailId { get; set; }

        public virtual MailConfigurationDbModel Mail { get; set; }

        public int? IssueId { get; set; }

        public virtual IssueConfigurationDbModel Issue { get; set; }
        public int? DefectId { get; set; }

        public virtual DefectConfigurationDbModel Defect { get; set; }

    }

}
