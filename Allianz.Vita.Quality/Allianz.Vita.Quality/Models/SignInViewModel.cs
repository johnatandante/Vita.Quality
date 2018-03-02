using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allianz.Vita.Quality.Models
{
    public class SignInViewModel
    {
        public string UserName { get; internal set; }
        public bool RememberMe { get; internal set; }
    }
}