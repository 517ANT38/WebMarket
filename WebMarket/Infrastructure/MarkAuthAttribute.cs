using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace WebMarket.Infrastructure
{
    public class MarkAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            IIdentity ident = filterContext.Principal.Identity;
            if (!ident.IsAuthenticated )
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"controller", "OrderRegistration"},
                    {"action",  "Authorization"},
                    {"returnUrl", filterContext.HttpContext.Request.RawUrl}
                });
            }

        }
    }
}