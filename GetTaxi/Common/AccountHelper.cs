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

        /// <summary>
        /// Check if current user in given roles
        /// </summary>
        /// <param name="roles">roles to check</param>
        /// <returns>Return true if user have all given roles</returns>
        public static bool IsInRoles(params int[] roles)
        {
            bool inRoles = false;

            UserData userData = currentUser;

            if (userData != null && roles.Length > 0)
            {
                int[] userRoles = userData.Roles;

                if (userRoles != null)
                {
                    int allowRoles = 0;
                    foreach (var role in roles)
                    {
                        if (userRoles.Contains(role))
                            allowRoles++;
                    }

                    if (allowRoles > 0 && allowRoles == roles.Length)
                        inRoles = true;
                }
            }

            return inRoles;
        }

        /// <summary>
        /// Check if current user in any of given roles
        /// </summary>
        /// <param name="roles">roles to check</param>
        /// <returns>Return true if user have any of given roles</returns>
        public static bool IsInAnyRoles(params int[] roles)
        {
            bool inRoles = false;

            UserData userData = currentUser;

            if (userData != null && roles.Length > 0)
            {
                int[] userRoles = userData.Roles;

                if (userRoles != null)
                {
                    int allowRoles = 0;
                    foreach (var role in roles)
                    {
                        if (userRoles.Contains(role))
                            allowRoles++;
                    }

                    if (allowRoles > 0)
                        inRoles = true;
                }
            }

            return inRoles;
        }

    }
}