using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace WebUI.Infrastructure
{
    public static class HttpResponseBaseExtensions
    {
        /// <summary>
        /// Overwrite authorization cookies
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseBase"></param>
        /// <param name="name">Login</param>
        /// <param name="rememberMe">Set permanent cookie</param>
        /// <param name="userData">Additional user data</param>
        /// <returns></returns>
        public static int SetAuthCookie<T>(this HttpResponseBase responseBase, string name, bool rememberMe, T userData)
        {
            // Create own cookies based on original
            var cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            //add additional user data
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
                ticket.IsPersistent, Json.Encode(userData), ticket.CookiePath);
            var encTicket = FormsAuthentication.Encrypt(newTicket);

            // Add new cookies
            cookie.Value = encTicket;

            responseBase.Cookies.Add(cookie);

            return encTicket.Length;
        }
    }
}