using System;

namespace Allianz.Vita.Client.Rest.Jira.DataModel.Auth
{
    public class LoginInfo
    {
        public int LoginCount { get; internal set; }
        public DateTime PreviousLogin { get; internal set; }

        public string SessionId { get; internal set; }

    }
}
