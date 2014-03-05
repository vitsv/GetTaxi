using Data.Domain;
using Data.Enumerators;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Common;
using WebUI.Infrastructure;
using WebUI.Models;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private ClientManager _manager;
        private ClientManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = new ClientManager();

                return _manager;
            }
        }

        public ActionResult Register()
        {
            if (AccountHelper.currentUser == null)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!string.IsNullOrEmpty(model.Phone) && !Manager.CheckIfPhoneUniq(model.Phone))
                ModelState.AddModelError("Phone", "Taki telefon został już zarejestrowany");

            if (ModelState.IsValid)
            {
                Client newUser = new Client();
                newUser.FirstName = model.Name;
                newUser.Phone = model.Phone;
                newUser.CreationDate = DateTime.Now;
                newUser.Password = model.Password;
                newUser.ActivateCode = Manager.GenerateSmsCode();
                newUser.IsActive = false;
                newUser.SmsSentCount = 1;

                var res = Manager.Add(newUser);

                if (!res.IsError)
                {
                    LogInUser(newUser);

                    //TODO wyslac SMS

                    return RedirectToAction("Activate");
                }
            }


            return View();

        }

        private void LogInUser(Client client)
        {
            FormsAuthentication.SetAuthCookie(client.Phone, true);

            UserData userData = new UserData
            {
                Phone = client.Phone,
                ID = client.ClientId,
                FullName = client.FullName
            };

            //Nadpisuje cookie dla przechowywania dodatkowych informacji
            Response.SetAuthCookie(client.Phone, true, userData);
        }



        public ActionResult Activate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Activate(ActivateModel model)
        {
            if (ModelState.IsValid)
            {
                if (Manager.ActivateNewUser(AccountHelper.currentUser.ID, model.Code))
                {
                    var user = Manager.GetUserById(AccountHelper.currentUser.ID);

                    AccountHelper.Logout();

                    LogInUser(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Code", "Kod jest nieprawidłowy");
                }
            }

            return View();
        }

        [HttpPost]
        public JsonResult ResendActivateSms()
        {
            if (Manager.ResendActivateSms(AccountHelper.currentUser.ID))
            {
                return Json(new { result = "OK", msg = "SMS został wysłany." });
            }
            else
            {
                return Json(new { result = "ERROR", msg = "SMS nie został wysłany! Proszę skontaktować się z biurem obsługi klienta." });
            }
        }
    }
}
