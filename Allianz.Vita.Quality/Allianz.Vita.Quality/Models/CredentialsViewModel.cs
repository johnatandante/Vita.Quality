using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allianz.Vita.Quality.Models
{
    public class CredentialsViewModel 
    {

        public string TFSUserName { get; set; }
        public string TFSDomainName { get; set; }
        public string TFSPassword { get; set; }

        public string ExchangeUserName { get; set; }
        public string ExchangeDomainName { get; set; }
        public string ExchangePassword { get; set; }

    }
}