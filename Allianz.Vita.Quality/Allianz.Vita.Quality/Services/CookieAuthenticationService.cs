using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Models;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace Allianz.Vita.Quality.Services
{
    public class CookieAuthenticationService : IService
    {

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

            cookie.Values[key] = Json.Encode(model);
            // renew
            cookie.Expires = DateTime.Now.AddDays(15);

            response.Cookies.Add(cookie);

        }

        private static HttpCookie EnsureMainCookie(HttpRequestBase request)
        {
            HttpCookie cookie = request.Cookies["Vita.Quality"];
            if (cookie == null)
            {
                cookie = new HttpCookie("Vita.Quality");                
            }

            return cookie;

        }
        
    }
}
