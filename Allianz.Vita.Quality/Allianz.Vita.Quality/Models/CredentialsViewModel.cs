﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Allianz.Vita.Quality.Models
{
    public class CredentialsViewModel 
    {
        public bool Initialized = false;

        public CredentialsViewModel()
        {
            TFSUserName = TFSPassword = TFSDomainName = string.Empty;
            ExchangeUserName = ExchangePassword = ExchangeDomainName = string.Empty;
        }
        
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

        public NetworkCredential TfsCredentials
        {
            get
            {
                return new NetworkCredential(TFSUserName, TFSPassword, TFSDomainName);
            }
        }

        public NetworkCredential MailCredentials
        {
            get
            {
                return new NetworkCredential(ExchangeUserName, ExchangePassword, ExchangeDomainName);
            }
        }
        
    }
}