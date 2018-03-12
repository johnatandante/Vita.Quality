using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Allianz.Vita.Quality.Attributes
{
    public class AuthorizedOnlyAttribute : AuthorizeAttribute
    {
        List<Type> Service = new List<Type>();

        public AuthorizedOnlyAttribute(params Type[] service)
        {
            Service.AddRange(service);
        }

        protected override sealed HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }

        protected override sealed bool AuthorizeCore(HttpContextBase httpContext)
        {
            IIdentityService auth = ServiceFactory.Get<IIdentityService>();
            return Service.All(s => auth.IsAuthenticatedOn(s));
            
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
                action = "Credentials";                
            }
            else
            {
                action = "SignIn";
            }
            
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "action", action },
                    { "controller", "Account" },
                    //{ "parameterName", "YourParameterValue" }
                });
            
        }

        public override sealed void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);           
        }
        
    }

}