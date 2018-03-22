using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Models;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace Allianz.Vita.Quality.Services
{
    public class CookieAuthenticationService : IService
    {

        static string CookieQuality = "Vita.Quality";

        IIdentityService Auth;

        public CookieAuthenticationService() : this(auth: null) { }

        public CookieAuthenticationService(IIdentityService auth)
        {
            Auth = auth ?? ServiceFactory.Get<IIdentityService>();
        }

        internal void EnsureCookie(HttpRequestBase request, HttpResponseBase response, string cookieName, bool peristent = true)
        {
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(cookieName, peristent);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            
            CredentialsViewModel model = GetData(request, cookieName);            
            if (!string.IsNullOrEmpty(model.TFSUserName))
            {
                Auth.AuthenticateOn(typeof(IDefectService), model.TfsCredentials);
            }
            if (!string.IsNullOrEmpty(model.ExchangeUserName))
            {
                Auth.AuthenticateOn(typeof(IMailService), model.MailCredentials);
            }
            if (!string.IsNullOrEmpty(model.JiraUserName))
            {
                Auth.AuthenticateOn(typeof(IIssueService), model.JiraCredentials);
            }

            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name,
                 ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, Json.Encode(cookieName));

            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            authCookie.Expires = DateTime.Now.AddMonths(3);
            
            response.Cookies.Add(authCookie);
        }

        internal void EnsureAuthentication(HttpRequestBase request, string username)
        {
            if (Auth.IsAuthenticated())
                return;
            
            Auth.LogOn(username);
            CredentialsViewModel model = GetData(request, username);

            if (!Auth.IsAuthenticatedOn(typeof(IMailService)) && !string.IsNullOrEmpty(model.ExchangeUserName))
                Auth.AuthenticateOn(typeof(IMailService), model.MailCredentials);
            if (!string.IsNullOrEmpty(model.TFSUserName))
                Auth.AuthenticateOn(typeof(IDefectService), model.TfsCredentials);
            if (!string.IsNullOrEmpty(model.JiraUserName))
                Auth.AuthenticateOn(typeof(IIssueService), model.JiraCredentials);
        }

        internal CredentialsViewModel GetData(HttpRequestBase request, string key)
        {
            HttpCookie cookie = EnsureMainCookie(request);
            if (!cookie.HasKeys || cookie.Values[key] == null) return new CredentialsViewModel();

            return (CredentialsViewModel)Json.Decode(cookie.Values[key], typeof(CredentialsViewModel));
        }

        internal void SetData(HttpRequestBase request, HttpResponseBase response, string key, CredentialsViewModel model)
        {
            HttpCookie cookie = EnsureMainCookie(request);

            if (!cookie.HasKeys || cookie.Values[key] == null)
                cookie.Values.Add(key, null);

            CredentialsViewModel stored = GetData(request, key);
            CredentialsViewModel modelToStore = UpdateWith(stored, model);

            cookie.Values[key] = Json.Encode(modelToStore);

            // renew
            cookie.Expires = DateTime.Now.AddDays(15);

            response.Cookies.Add(cookie);

        }

        private static CredentialsViewModel UpdateWith(CredentialsViewModel stored, CredentialsViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ExchangeUserName))
            {
                stored.ExchangeUserName = model.ExchangeUserName;
                stored.ExchangeDomainName = model.ExchangeDomainName;
                stored.ExchangePassword = model.ExchangePassword;
            }

            if (!string.IsNullOrEmpty(model.TFSUserName))
            {
                stored.TFSUserName = model.TFSUserName;
                stored.TFSDomainName = model.TFSDomainName;
                stored.TFSPassword = model.TFSPassword;
            }

            if (!string.IsNullOrEmpty(model.JiraUserName))
            {
                stored.JiraUserName = model.JiraUserName;
                stored.JiraPassword = model.JiraPassword;
            }

            return stored;
        }

        private static HttpCookie EnsureMainCookie(HttpRequestBase request)
        {
            HttpCookie cookie = request.Cookies[CookieQuality];
            if (cookie == null)
            {
                cookie = new HttpCookie(CookieQuality);                
            }

            return cookie;

        }
        
    }
}
