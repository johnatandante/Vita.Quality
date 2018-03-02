using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IIdentityService : IService
    {
        
        IUserCredentials LogOn(IIdentity credentials);

        IUserCredentials AuthenticateOn(IService service, NetworkCredential networkCredential);
        
        bool Logoff(IService service);

        NetworkCredential GetCredentialsFor(IService service);

        bool IsAuthenticatedOn(Type service);

        bool IsAuthenticated();

    }
}
