using System;

namespace Allianz.Vita.Quality.Business.Services.Authentication
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException() : base("Not Authenticated") { }

        public AuthenticationException(string message, Exception inner) : base(message, inner) { }

    }
}
