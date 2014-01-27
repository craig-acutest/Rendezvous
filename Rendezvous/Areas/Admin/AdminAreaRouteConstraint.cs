using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Rendezvous.Areas.Admin
{
    public class AdminAreaRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return Validate(values["area"]);
        }

        private bool Validate(RouteValueDictionary value)
        {
            return true;
        }
    }
}