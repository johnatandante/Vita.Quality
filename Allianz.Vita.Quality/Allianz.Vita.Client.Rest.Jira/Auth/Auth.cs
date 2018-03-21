using Allianz.Vita.Client.Rest.Jira.DataModel.Auth;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace Allianz.Vita.Client.Rest.Jira.Auth
{
    class Auth : RestClient
    {
        WebSudo webSudo;
        Session session;

        LoginInfo info;

        public Auth(HttpClient httpClient = null) 
            : base(httpClient)
        {
            info = new LoginInfo();

            webSudo = new WebSudo();
            session = new Session(this);
        }

        public bool Authenticated
        {
            get
            {
                return !string.IsNullOrEmpty(info.SessionId);
            }
        }

        public string SessionName { get; internal set; }
        public string SessionId { get; internal set; }

        public async Task<LoginInfo> Login(NetworkCredential networkCredentials)
        {
            info = await session.Login(networkCredentials);

            if (Authenticated)
            {
                SessionName = Session.DefaultSessionName;
                SessionId = info.SessionId;
            }

            return info;
        }

        public void Logout()
        {
            session.Logout();
            info = new LoginInfo(); 

        }

        public async Task<LoginInfo> GetCurrentUser()
        {
            return Authenticated ? info : await session.GetCurrentUser();
        }
    }
}
