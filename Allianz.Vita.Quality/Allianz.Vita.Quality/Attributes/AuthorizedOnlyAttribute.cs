using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Attributes
{
    public class AuthorizedOnlyAttribute : AuthorizeAttribute
    {
        List<Type> Service = new List<Type>();

        public AuthorizedOnlyAttribute(params Type[] service)
        {
            Service.AddRange(service);
        }
        
        protected override sealed void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            IIdentityService auth = ServiceFactory.Get<IIdentityService>();

            if (auth.IsAuthenticated())
            {
                if (Service.Any(s => !auth.IsAuthenticatedOn(s)))
                {
                    var viewResult = new ViewResult
                    {
                        ViewName = "~/Views/Account/Credentials.cshtml"
                    };
                    filterContext.Result = viewResult;
                }
            } else {
                var viewResult = new ViewResult
                {
                    ViewName = "~/Views/Account/SignIn.cshtml"
                };
                filterContext.Result = viewResult;
            } 
        }
    }

}