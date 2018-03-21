using Allianz.Vita.Client.Rest.Jira.Auth.Request;
using Allianz.Vita.Client.Rest.Jira.DataModel.Auth;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.Auth
{
    /// <summary>
    /// Implement a REST resource for acquiring a session cookie.
    /// Current user
    /// GET /rest/auth/1/session
    /// Logout
    /// DELETE /rest/auth/1/session
    /// Login
    /// POST /rest/auth/1/session
    /// </summary>
    class Session
    {
        LoginInfo loginInfo;

        RestClient module;

        static string SessionPath = "rest/auth/1/session";

        public static string DefaultSessionName = "JSESSIONID";

        public Session(RestClient auth)
        {
            this.module = auth;
        }

        internal async Task<LoginInfo> Login(NetworkCredential credentials)
        {
            loginInfo = new LoginInfo();

            LoginRequest login = new LoginRequest() { username = credentials.UserName, password = credentials.Password };
            HttpResponseMessage response = await module.Client.PostAsJsonAsync<LoginRequest>(Session.SessionPath, login);
            if (response.IsSuccessStatusCode)
            {
                Response.LoginResponse sessionInfo = await response.Content.ReadAsAsync<Response.LoginResponse>();
                loginInfo.SessionId = sessionInfo.session.value;
                loginInfo.LoginCount = sessionInfo.loginInfo.loginCount;
                loginInfo.PreviousLogin = sessionInfo.loginInfo.previousLoginTime;
            }
            
            return loginInfo;

        }

        internal async Task<LoginInfo> GetCurrentUser()
        {
            HttpResponseMessage response = await module.Client.GetAsync(Session.SessionPath);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                Response.CurrentUserResponse result = await response.Content.ReadAsAsync<Response.CurrentUserResponse>();                
                loginInfo.LoginCount = result.loginInfo.loginCount;
                loginInfo.PreviousLogin = result.loginInfo.previousLoginTime;
            }

            return loginInfo;
        }

        internal void Logout()
        {
            loginInfo = null;
            module.Client.DeleteAsync(Session.SessionPath);
        }
        
    }
}
