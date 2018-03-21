using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using System;
using System.Net;

namespace Allianz.Vita.Quality.Business.Interfaces.Service
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

        NetworkCredential GetCredentialsFor<T>() where T : IService;
    }
}
