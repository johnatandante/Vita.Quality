using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Attributes
{
    class CustomAuthorizedAttribute : AuthorizeAttribute
    {

        List<Type> Service = new List<Type>();

        CustomAuthorizedAttribute(Type[] service)
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

        public override sealed void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

    }
}