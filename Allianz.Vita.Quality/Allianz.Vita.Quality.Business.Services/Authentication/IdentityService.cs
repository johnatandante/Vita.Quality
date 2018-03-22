using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Net;

namespace Allianz.Vita.Quality.Business.Services.Authentication
{
    public class IdentityService : IIdentityService
    {

        IUserCredentials Current { get; set; }

        IItemFactory Factory;

        public IdentityService() : this( factory: null, identity: null)
        {
            
        }

        public IdentityService(IItemFactory factory, IUserCredentials identity)
        {
            Factory = factory ?? ServiceFactory.Get<IItemFactory>();

            Current = identity ?? Factory.GetNewUserCredential( new NetworkCredential());

        }

        public IUserCredentials AuthenticateOn(Type service, NetworkCredential networkCredential)
        {
            if (IsAuthenticatedOn(service))
                return Current;
            
            Current.AddIdentityFor(networkCredential, service);
            
            return Current;

        }

        public IUserCredentials AuthenticateOn<T>(NetworkCredential networkCredential)
            where T : IService
        {
            return AuthenticateOn(typeof(T), networkCredential);
        }

        public NetworkCredential GetCredentialsFor(IService service)
        {
            return IsAuthenticatedOn(service.GetType()) ?
                Current.GetCredentialFor(service.GetType()) : new NetworkCredential();
            
        }

        public NetworkCredential GetCredentialsFor<T>() where T : IService
        {
            return IsAuthenticatedOn(typeof(T)) ?
                Current.GetCredentialFor(typeof(T)) : new NetworkCredential();
        }


        public bool IsAuthenticatedOn(Type service)
        {
            return Current.GetCredentialFor(service) != null;
        }

        public bool IsAuthenticated()
        {
            return Current.IsAuthenticated;
        }

        public bool Logoff(Type service)
        {
            return Current.Forget(service);
        }

        public bool Logoff()
        {
            Current = Factory.GetNewUserCredential(new NetworkCredential());

            return true;
        }

        public IUserCredentials LogOn(string identity)
        {            
            Current = Factory.GetNewUserCredential(new NetworkCredential() { UserName = identity });
            return Current;

        }

        public bool IsValidUser(string userName)
        {
            return !string.IsNullOrEmpty(userName) && userName.Length > 3;
        }

        public bool IsValidAccount(string userName, string password)
        {
            return !(!IsValidUser(userName) || string.IsNullOrEmpty(password));
        }

    }
}
