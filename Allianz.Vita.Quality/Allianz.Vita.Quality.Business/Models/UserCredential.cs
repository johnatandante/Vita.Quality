using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;

namespace Allianz.Vita.Quality.Business.Models
{
    public class UserCredential : IUserCredentials
    {
        Dictionary<Type, NetworkCredential> _Identities;

        string _AuthenticationType;

        public UserCredential(string name, string authType = "")
        {
            Name = name;
            _Identities = new Dictionary<Type, NetworkCredential>();
            _AuthenticationType = authType;
        }

        public string Name { get; set; }
                
        public string AuthenticationType
        {
            get
            {
                return _AuthenticationType;
            }
        }

        public bool IsAuthenticated {
            get
            {
                return !string.IsNullOrEmpty(Name);
            }
        }

        public enum AuthenticationMode { Classic }

        public NetworkCredential GetCredentialFor(Type service)
        {
            return _Identities.ContainsKey(service) ? _Identities[service] : null;

        }

        public void AddIdentityFor(NetworkCredential credential, Type service)
        {
            if (!_Identities.ContainsKey(service))
                _Identities.Add(service, null);

            _Identities[service] = credential;
        }

        public bool Forget(Type service)
        {
            return _Identities.Remove(service);
        }
    }
}
