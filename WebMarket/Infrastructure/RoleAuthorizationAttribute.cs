using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.Models.Adapters;

namespace WebMarket.Controllers
{
    
    public class RoleAuthorizationAttribute: AuthorizeAttribute
    {
        private string role;
        public RoleAuthorizationAttribute(string role)
        {
            this.role = role;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IAdapterClient client = new AdapterClient();
            var s=client.Get(httpContext.User.Identity.Name);
            if (s.Role == role) return true;
            return false;
        }
    }
}