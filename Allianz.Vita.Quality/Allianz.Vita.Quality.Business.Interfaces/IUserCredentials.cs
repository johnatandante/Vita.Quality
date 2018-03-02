using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IUserCredentials : IIdentity
    {

        NetworkCredential GetCredentialFor(Type service);

        void AddIdentityFor(NetworkCredential credential, Type service);

        bool Forget(Type service);

    }
}
