using Allianz.Vita.Client.Rest.Jira.DataModel;
using Allianz.Vita.Client.Rest.Jira.DataModel.Auth;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira
{
    /// <summary>
    /// Jira Client with Cookie based Auth policy
    /// </summary>
    /// <see cref="https://docs.microsoft.com/it-it/aspnet/web-api/overview/security/external-authentication-services"/>    
    public class Jira : IDisposable
    {

        static Jira _instance;

        HttpClient client;

        CookieContainer cookieContainer;
        HttpClientHandler ClientHandler;

        internal HttpClient Client
        {
            get
            {
                if (client == null)
                    client = GetNewClient();

                return client;
            }
        }
        
        internal static Jira Instance
        {
            get
            {
                return _instance;
            }
        }

        public NetworkCredential Credential { get; set; }
        public Uri Uri { get; set; }

        public bool IsAuthenticated
        {
            get
            {
                return auth.Authenticated;
            }
        }

        Auth.Auth auth;
        Api2.Issue.Issue issue;
        Api2.Configuration configuration;

        public Jira(Uri uri, NetworkCredential credential = null)
        {
            _instance = this;



            Uri = uri;
            Credential = credential;

            auth = new Auth.Auth(Client);
            issue = new Api2.Issue.Issue(Client);

            configuration = new Api2.Configuration(Client);

        }

        internal HttpClient GetNewClient()
        {
            cookieContainer = new CookieContainer();
            ClientHandler = new HttpClientHandler() { CookieContainer = cookieContainer };
            
            HttpClient client = new HttpClient(ClientHandler)
            {
                BaseAddress = Uri
            };
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public async Task<LoginInfo> Login()
        {
            LoginInfo result = await auth.Login(Credential);
            cookieContainer.Add(Uri, new Cookie(auth.SessionName, auth.SessionId));

            return result;
        }

        public void Dispose()
        {
            if (auth.Authenticated)
            {
                auth.Logout();
            }
            
            Client.Dispose();
            ClientHandler.Dispose();
            cookieContainer = null;

            auth = null;

        }

        public async Task<IEnumerable<Issue>> GetIssuesFromJqlAsync(string jqlquery, int startAt = 0, int maxResults = 0)
        {
            List<Issue> list = new List<Issue>();
            Issue item = await GetIssueAsync("PRLIFE-16338");
            if(!string.IsNullOrEmpty(item.Id))
                list.Add(item);

            return list;
        }

        public async Task<Issue> GetIssueAsync(string idOrKey)
        {
            return await issue.Get(idOrKey);
        }

        public async Task<LoginInfo> GetCurrentUser()
        {
            return await auth.GetCurrentUser();
        }

        public async Task<Configuration> GetConfiguration()
        {
            return await configuration.Get();
        }

        public async Task<bool> IsUp()
        {
            try
            {
                return await GetConfiguration() != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
