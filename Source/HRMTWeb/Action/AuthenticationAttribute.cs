using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HRMTWeb.Action
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AppSessionHelper.User == null)
                filterContext.Result = new RedirectToRouteResult("Login", new RouteValueDictionary { { "from", filterContext.HttpContext.Request.Path } });
            base.OnActionExecuting(filterContext);
        }
    }
}