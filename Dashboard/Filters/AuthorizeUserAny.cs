using System.Web;
using System.Web.Mvc;
using Dashboard.Common;

namespace Dashboard.Filters
{
    /// <summary>
    /// Check if user have account and any of necessary rights
    /// </summary>
    public class AuthorizeUserAny : AuthorizeAttribute
    {
        private int[] necessaryRoles;

        public AuthorizeUserAny(params int[] roles)
        {
            necessaryRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AccountHelper.IsInAnyRoles(necessaryRoles);
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult { Data = "_Denied_", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                    filterContext.Result = new RedirectResult(urlHelper.Action("AccessDenied", "Account", new { area = "" }));
                }
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult { Data = "_Logon_", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                    string url = urlHelper.RequestContext.HttpContext.Request.Url.PathAndQuery;
                    filterContext.Result = new RedirectResult(urlHelper.Action("LogOn", "Account", new { area = "", returnUrl = url }));
                }
            }
        }

    }
}