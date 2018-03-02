using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Net;
using System.Security.Principal;

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

        public IUserCredentials AuthenticateOn(IService service, NetworkCredential networkCredential)
        {
            Type type = service.GetType();
            if (IsAuthenticatedOn(service.GetType()))
                return Current;
            
            Current.AddIdentityFor(networkCredential, service.GetType());
            
            return Current;

        }

        public NetworkCredential GetCredentialsFor(IService service)
        {
            return IsAuthenticatedOn(service.GetType()) ?
                Current.GetCredentialFor(service.GetType()) : new NetworkCredential();
            
        }

        public bool IsAuthenticatedOn(Type service)
        {
            return Current.GetCredentialFor(service) != null;
        }

        public bool IsAuthenticated()
        {
            return Current.IsAuthenticated;
        }

        public bool Logoff(IService service)
        {
            return Current.Forget(service.GetType());
        }

        public IUserCredentials LogOn(IIdentity identity)
        {            
            return Factory.GetNewUserCredential(new NetworkCredential() { UserName = identity.Name });
        }
    }
}
