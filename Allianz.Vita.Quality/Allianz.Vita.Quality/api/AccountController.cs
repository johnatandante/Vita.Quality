using Allianz.Vita.Quality.api.Response;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Extensions;
using System.Net;
using System.Web.Http;

namespace Allianz.Vita.Quality.api
{
    public class AccountController : ApiController
    {
        IIdentityService Service
        {
            get { return ServiceFactory.Get<IIdentityService>(); }
        }

        public AccountResponse Get()
        {
            return this.HandleGetRequest<AccountResponse, object>(() =>
            {
                return User.Identity;
            });
        }

        [HttpGet]
        public ArryayResponse GetSupportedServices()
        {
            return this.HandleGetRequest<ArryayResponse, object[]>(() =>
            {
                return Service.GetSupportedServices();
            });
        }

        [HttpGet]
        public AccountResponse GetIsLoggedOn()
        {
            return this.HandleGetRequest<AccountResponse, object>(() =>
            {
                return Service.IsAuthenticated();
            });
        }

        [HttpPost]
        public AccountResponse LogOn(string username)
        {
            return this.HandleGetRequest<AccountResponse, object>(() =>
            {
                return Service.LogOn(username);
            });

        }

        public AccountResponse AuthenticateOn(string serviceName, string username, string password, string domain)
        {
            return this.HandleGetRequest<AccountResponse, object>(() =>
            {                
                NetworkCredential credential = new NetworkCredential() { UserName = username, Password = password, Domain = domain };

                return Service.AuthenticateOn(serviceName, credential);
            });
        }

        [HttpDelete]
        public AccountResponse LogOff(string username)
        {
            return this.HandleGetRequest<AccountResponse, object>(() =>
            {
                return Service.LogOn(username) != null;
            });

        }

        public AccountResponse CredentialError()
        {
            return this.HandleGetRequest<AccountResponse, object>(() =>
            {
                return "Not authenticated";
            });
        }

        public AccountResponse SignInError()
        {
            return this.HandleGetRequest<AccountResponse, object>(() =>
            {
                return "Not logged in";
            });
        }

    }
}
