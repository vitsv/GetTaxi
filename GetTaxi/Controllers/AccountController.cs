﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebUI.Models;
using Managers;
using Data.Domain;
using WebUI.Infrastructure;
using WebUI.Common;


namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private ClientManager Manager
        {
            get
            {
                return new ClientManager();
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
                if (Membership.ValidateUser(model.Phone, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Phone, model.RememberMe);

                    Client user = Manager.GetClientByPhone(model.Phone);

                    UserData userData = new UserData
                    {
                        Phone = user.Phone,
                        ID = user.ClientId,
                        FullName = user.FullName
                    };

                    //Nadpisuje cookie dla przechowywania dodatkowych informacji
                    Response.SetAuthCookie(model.Phone, true, userData);

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
