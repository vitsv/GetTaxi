using System.Linq;
using System.Web;
using System.Web.Security;
using WebUI.Models;
using System.Web.Helpers;

namespace WebUI.Common
{
    public class AccountHelper
    {
        private AccountHelper() { }

        /// <summary>
        /// Contains data of currently logged user
        /// </summary>
        public static UserData currentUser
        {
            get
            {
                return GetUserData();
            }
        }

        private static UserData GetUserData()
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

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }

    }
}