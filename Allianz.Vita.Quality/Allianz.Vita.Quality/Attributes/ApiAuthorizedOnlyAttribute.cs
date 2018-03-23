using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Allianz.Vita.Quality.Attributes
{
    class ApiAuthorizedOnlyAttribute: CustomAuthorizedAttribute
    {

        public ApiAuthorizedOnlyAttribute(params Type[] service) : base(service)
        {
        }
        
        /// <summary>
        /// Filter on reference
        /// </summary>
        /// <see cref="https://stackoverflow.com/questions/977071/redirecting-unauthorized-controller-in-asp-net-mvc"/>
        protected override sealed void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            string action = string.Empty;

            IIdentityService auth = ServiceFactory.Get<IIdentityService>();
            if (ServiceFactory.Get<IIdentityService>().IsAuthenticated())
            {
                action = "CredentialError";
            }
            else
            {
                action = "SignInError";
            }

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "action", action },
                    { "controller", "Account" },
                    //{ "parameterName", "YourParameterValue" }
                });

        }
        
    }
}