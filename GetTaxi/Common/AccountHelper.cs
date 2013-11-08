using Translator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace Translator.Common
{
    public class AccountHelper
    {
        public static UserData GetCurrentUser()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (null == cookie)
                    return null;

                var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                if (!string.IsNullOrEmpty(decrypted.UserData))
                    return Json.Decode<UserData>(decrypted.UserData);
            }
            return null;
        }
    }
}