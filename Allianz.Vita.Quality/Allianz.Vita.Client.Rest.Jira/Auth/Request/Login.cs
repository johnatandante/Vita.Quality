using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.Auth.Request
{
    /// <summary>
    /// {
    ///    "username": "fred",
    ///    "password": "freds_password"
    ///}
    /// </summary>
    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
