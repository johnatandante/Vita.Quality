using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.DataModel.Auth
{
    public class LoginInfo
    {
        public int LoginCount { get; internal set; }
        public DateTime PreviousLogin { get; internal set; }

        public string SessionId { get; internal set; }

    }
}
