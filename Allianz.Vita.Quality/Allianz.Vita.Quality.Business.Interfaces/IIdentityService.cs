using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IIdentityService : IService
    {
        
        IUserCredentials LogOn(string credentials);

        IUserCredentials AuthenticateOn(Type service, NetworkCredential networkCredential);
        
        bool Logoff(Type service);

        bool Logoff();

        NetworkCredential GetCredentialsFor(IService service);

        bool IsAuthenticatedOn(Type service);

        bool IsAuthenticated();

        bool IsValidUser(string userName);

        bool IsValidAccount(string userName, string password);

    }
}
