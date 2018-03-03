using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Allianz.Vita.Quality.Models
{
    public class CredentialsViewModel 
    {
        public bool Initialized = false;

        [Display(Name = "User Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string TFSUserName { get; set; }

        [Display(Name = "User Domain")]
        public string TFSDomainName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string TFSPassword { get; set; }

        [Display(Name = "Account Name")]
        public string ExchangeUserName { get; set; }

        [Display(Name = "Account Domain")]
        public string ExchangeDomainName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string ExchangePassword { get; set; }

    }
}