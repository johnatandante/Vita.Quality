using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace Allianz.Vita.Quality.Services
{
    class CookieAuthenticationService
    {

        IIdentityService Auth;

        public CookieAuthenticationService() : this(auth: null) { }

        public CookieAuthenticationService(IIdentityService auth)
        {
            Auth = auth ?? ServiceFactory.Get<IIdentityService>();
        }

        internal void EnsureCookie(HttpRequestBase request, HttpResponseBase response, string cookieName, bool peristent = true)
        {
            HttpCookie authCookie;
            string newData = "";
            if (!request.Cookies.AllKeys.Any(k => k == cookieName))
            {
                FormsAuthentication.SetAuthCookie(cookieName, peristent);
                newData = Json.Encode(new CredentialsViewModel());
            }

            authCookie = FormsAuthentication.GetAuthCookie(cookieName, peristent);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (!string.IsNullOrEmpty(ticket.UserData))
            {
                newData = ticket.UserData;
                CredentialsViewModel decoded = (CredentialsViewModel) Json.Decode(newData, typeof(CredentialsViewModel));
                if (decoded != null && !string.IsNullOrEmpty(decoded.TFSUserName))
                {
                    Auth.AuthenticateOn(typeof(IDefectService), new System.Net.NetworkCredential(decoded.TFSUserName, decoded.TFSDomainName, decoded.TFSPassword));
                }
                if (decoded != null && !string.IsNullOrEmpty(decoded.ExchangeUserName))
                {
                    Auth.AuthenticateOn(typeof(IMailService), new System.Net.NetworkCredential(decoded.ExchangeUserName, decoded.ExchangeDomainName, decoded.ExchangePassword));
                }

            }

            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name,
                 ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, newData);

            authCookie.Value = FormsAuthentication.Encrypt(newTicket);
            authCookie.Expires = DateTime.Now.AddMonths(3);
            
            response.Cookies.Add(authCookie);
        }

        internal CredentialsViewModel Get(HttpRequestBase request, string cookieName, bool persistent = true)
        {
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(cookieName, persistent);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            if (string.IsNullOrEmpty(ticket.UserData))
                return new CredentialsViewModel();
            else
                return (CredentialsViewModel)Json.Decode(ticket.UserData, typeof(CredentialsViewModel));
        }

        internal CredentialsViewModel GetCookie(HttpRequestBase request, string name)
        {
            HttpCookie cookie = request.Cookies["Vita.Quality"];
            if (cookie == null) return new CredentialsViewModel();

            return (CredentialsViewModel)Json.Decode(cookie.Values[name], typeof(CredentialsViewModel));
        }

        internal void SetCookieData(HttpRequestBase request, string name, CredentialsViewModel model)
        {
            HttpCookie myCookie = new HttpCookie("Vita.Quality");

            HttpCookie cookie = request.Cookies[name];
            if (cookie == null) return new CredentialsViewModel();

            return (CredentialsViewModel)Json.Decode(cookie.Value, typeof(CredentialsViewModel));
        }

        internal void Persist(HttpRequestBase request, HttpResponseBase response,  CredentialsViewModel model, string cookieName, bool persistent)
        {
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(cookieName, persistent );
            authCookie.Expires = DateTime.Now.AddMonths(3);

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            string newData = Json.Encode(model);

            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name,
                 ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, newData);

            authCookie.Value = FormsAuthentication.Encrypt(newTicket);

            response.Cookies.Add(authCookie);
        }
    }
}
