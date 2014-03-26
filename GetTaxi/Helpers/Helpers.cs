using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Helpers
{
    public static class Helpers
    {
        public static string ValidateWithClass(this HtmlHelper html, string propertyName)
        {
            ModelState state = html.ViewData.ModelState[propertyName];
            if (state != null && state.Errors.Count > 0)
                return "has-error";

            return "";
        }
    }
}