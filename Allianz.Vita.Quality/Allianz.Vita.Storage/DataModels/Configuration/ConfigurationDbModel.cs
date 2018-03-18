using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    public class ConfigurationDbModel
    {
        public int ID { get; set; }

        public MailConfigurationDbModel Mail { get; set; }

        public IssueConfigurationDbModel Issue { get; set; }

        public DefectConfigurationDbModel Defect { get; set; }

        public DateTime StartDate { get; set; }

    }

}
