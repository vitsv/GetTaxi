using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace WebUI.Models
{
    public class UserDataModelBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
        ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");
            if (controllerContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                var cookie = controllerContext
                    .RequestContext
                    .HttpContext
                    .Request
                    .Cookies[FormsAuthentication.FormsCookieName];

                if (null == cookie)
                    return null;

                var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                if (!string.IsNullOrEmpty(decrypted.UserData))
                    return Json.Decode<T>(decrypted.UserData);
            }
            return null;
        }
    }
}