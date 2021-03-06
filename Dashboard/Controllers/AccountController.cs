﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Dashboard.Models;
using Managers;
using Data.Domain;
using Dashboard.Infrastructure;
using Dashboard.Common;


namespace Dashboard.Controllers
{
    public class AccountController : Controller
    {
        private UserManager Manager
        {
            get
            {
                return new UserManager();
            }
        }
        /// <summary>
        /// Logowanie usera
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOn()
        {
            LogOnModel model = new LogOnModel();

            return View(model);
        }


        /// <summary>
        /// Logowanie usera
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Login, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);

                    User user = Manager.GetUserByLogin(model.Login);

                    UserData userData = new UserData
                    {
                        ID = user.UserId,
                        FullName = user.FullName,
                        Roles = user.Roles
                    };

                    //Nadpisuje cookie dla przechowywania dodatkowych informacji
                    Response.SetAuthCookie(model.Login, model.RememberMe, userData);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login or password!");
                }
            }

            return View(model);
        }


        /// <summary>
        /// Wylogowuje usera
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogOn", "Account");
        }

        public PartialViewResult UserBox()
        {
            return PartialView("_LogOnPartial", AccountHelper.currentUser);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
