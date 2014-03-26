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

        public PartialViewResult Register()
        {
            var model = new RegisterModel();
            return PartialView("Register", model);
        }

        [HttpPost]
        public PartialViewResult Register(RegisterModel model)
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

                    //TODO wyslac SMS

                    return PartialView("Partial/_registerSuccessPartial", newUser.ClientId);
                }
            }


            return PartialView("Partial/_registerPartial", model);

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

            Manager.UpdateLastLoginDate(client.ClientId);
            

            //Nadpisuje cookie dla przechowywania dodatkowych informacji
            Response.SetAuthCookie(client.Phone, true, userData);
        }


        public PartialViewResult Activate(int userId)
        {
            var model = new ActivateModel();
            model.IdUser = userId;
            return PartialView("Activate", model);
        }

        [HttpPost]
        public ActionResult Activate(ActivateModel model)
        {
            if (ModelState.IsValid)
            {
                if (Manager.ActivateNewUser(model.IdUser, model.Code))
                {
                    var user = Manager.GetUserById(model.IdUser);

                    AccountHelper.Logout();

                    LogInUser(user);

                    return PartialView("Partial/_activateSuccessPartial");
                }
                else
                {
                    ModelState.AddModelError("Code", "Kod jest nieprawidłowy");
                }
            }

            return PartialView("Partial/_activatePartial", model);
        }

        [HttpPost]
        public JsonResult ResendActivateSms(int userId)
        {
            if (Manager.ResendActivateSms(userId))
            {
                return Json(new { result = "OK", msg = "SMS został wysłany." });
            }
            else
            {
                return Json(new { result = "ERROR", msg = "SMS nie został wysłany! Proszę skontaktować się z biurem obsługi klienta." });
            }
        }

        public PartialViewResult RememberPassSendSMS()
        {
            ViewBag.AccountNotExistMsg = false;
            var model = new RemeberPassModel();
            return PartialView("RememberPassSendSMS", model);
        }

        [HttpPost]
        public ActionResult RememberPassSendSMS(RemeberPassModel model)
        {
            ViewBag.AccountNotExistMsg = false;
            if (ModelState.IsValid)
            {
                var res = Manager.RememberPassSendSMS(model.Phone);
                if (res.IsError)
                {
                    if (res.ErrorMessage == "NOTEXIST")
                        ViewBag.AccountNotExistMsg = true;
                    else
                    {
                        ModelState.AddModelError("", res.ErrorMessage);
                    }
                }
                else
                {
                    var user = Manager.GetClientByPhone(model.Phone);
                    return PartialView("Partial/_rememberPassSendSMSSuccess", user.ClientId);
                }

            }

            return PartialView("Partial/_rememberPassSendSmsPartial", model);
        }

        public PartialViewResult RememberPassConfirm(int userId)
        {
            var model = new ActivateModel();
            model.IdUser = userId;
            return PartialView("RememberPassSendConfirm", model);
        }

        [HttpPost]
        public ActionResult RememberPassConfirm(ActivateModel model)
        {
            if (ModelState.IsValid)
            {
                if (Manager.RepairPass(model.IdUser, model.Code))
                {
                    var user = Manager.GetUserById(model.IdUser);

                    AccountHelper.Logout();

                    LogInUser(user);

                    return PartialView("Partial/_rememberPassConfirmSuccessPartial");
                }
                else
                {
                    ModelState.AddModelError("Code", "Kod jest nieprawidłowy");
                }
            }

            return PartialView("Partial/_activatePartial", model);
        }
    }
}
