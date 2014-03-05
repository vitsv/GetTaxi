using Data.Domain;
using Data.Enumerators;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Common;
using WebUI.Models.Order;
using WebUI.Infrastructure;
using System.Text.RegularExpressions;
using System.Web.Security;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class OrderController : Controller
    {
        private OrderManager _manager;
        private OrderManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = new OrderManager();

                return _manager;
            }
        }

        private ClientManager _managerClient;
        private ClientManager ManagerClient
        {
            get
            {
                if (_managerClient == null)
                    _managerClient = new ClientManager();

                return _managerClient;
            }
        }

        public PartialViewResult Create()
        {
            PrepareCreate();
            return PartialView("Create");
        }

        private void PrepareCreate()
        {
            ViewBag.CityList = Manager.RepoGeneric.GetAll<City>().OrderBy(c => c.DisplayOrder).Select(c => new SelectListItem { Text = c.Name, Value = c.CityId.ToString() }).ToList();

            var orderTimeHour = DateTime.Now.AddMinutes(5).Hour;
            List<SelectListItem> hoursList = new List<SelectListItem>();
            for (int i = orderTimeHour; i <= 23; i++)
            {
                hoursList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = (i == orderTimeHour) });
            }

            List<SelectListItem> hoursListTomorrow = new List<SelectListItem>();
            for (int i = 0; i <= 23; i++)
            {
                hoursListTomorrow.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            var orderTimeMinute = DateTime.Now.AddMinutes(5).Minute;
            List<SelectListItem> minutesList = new List<SelectListItem>();
            minutesList.Add(new SelectListItem { Text = "00", Value = "0", Selected = (orderTimeMinute >= 0 && orderTimeMinute < 5) });
            minutesList.Add(new SelectListItem { Text = "05", Value = "5", Selected = (orderTimeMinute >= 5 && orderTimeMinute < 10) });
            for (int i = 10; i <= 55; i = i + 5)
            {
                minutesList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = (orderTimeMinute >= i && orderTimeMinute < i + 5) });
            }

            ViewBag.HoursList = hoursList;
            ViewBag.HoursListTomorrow = hoursListTomorrow;
            ViewBag.MinutesList = minutesList;
            ViewBag.Companies = Manager.RepoGeneric.GetAll<Company>().OrderBy(c => c.Name).ToList();
        }


        [HttpPost]
        [Authorize]
        public ActionResult Create(EditableOrder model)
        {
            DateTime serveCarTime;
            if (model.PlannedDay == 1)
            {
                serveCarTime = DateTime.Parse(string.Format("{0} {1}:{2}", DateTime.Today.ToShortDateString(), model.PlannedHour, model.PlannedMinute));

            }
            else
            {
                serveCarTime = DateTime.Parse(string.Format("{0} {1}:{2}", DateTime.Now.AddDays(1).ToShortDateString(), model.PlannedHour, model.PlannedMinute));
            }

            if (serveCarTime <= DateTime.Now)
                ModelState.AddModelError("PlannedDay", "Niestety nie możemy podać samochód na tą godzinę");

            if (ModelState.IsValid)
            {
                Order newOrder = new Order();
                newOrder.ClientId = AccountHelper.currentUser.ID;
                newOrder.Status = (int)GlobalEnumerator.OrderStatus.Created;
                newOrder.TimeCreated = DateTime.Now;
                newOrder.IsPrepaid = false;



                if (serveCarTime > DateTime.Now.AddMinutes(30))
                    newOrder.IsPlanned = true;
                newOrder.Deadline = serveCarTime;


                Address address = new Address();
                address.CityFrom = model.CityFrom;
                address.AddressFrom = model.AddressFrom;
                if (!string.IsNullOrEmpty(model.AddressTo))
                    address.AddressTo = model.AddressTo;
                newOrder.Address = address;

                newOrder.UserComment = model.Comment;

                foreach (var comp in model.Companies)
                {
                    newOrder.OrderCompany.Add(new OrderCompany { CompanyId = comp });
                }

                OrderProperties properties = new OrderProperties();

                properties.OrderClass = (int)GlobalEnumerator.OrderClass.Fastest;
                properties.Priority = (int)GlobalEnumerator.OrderPriority.Normal;
                properties.Childer = model.Childer;
                properties.Nosmoking = model.NoSmoking;
                properties.Card = model.Card;
                properties.Animal = model.Animal;
                properties.English = model.English;

                newOrder.OrderProperties = properties;

                var res = Manager.AddOrder(newOrder);

                if (!res.IsError)
                    return Json(new { result = "OK", order_id = newOrder.OrderId });
                else
                    return Json(new { result = "ERROR", msg = res.ErrorMessage });
            }

            return Json(new { result = "ERROR", msg = "Unknown" });
        }

        public PartialViewResult Status(int id)
        {
            var order = Manager.GetOrderById(id);
            if (order == null)
                throw new Exception(string.Format("Order with id {0} was not found", id));
            var model = new OrderStatusModel();

            model.OrderId = id;
            model.Status = (GlobalEnumerator.OrderStatus)order.Status;
            model.StatusText = GlobalEnumerator.GetEnumName<GlobalEnumerator.OrderStatus>(model.Status);
            model.PhoneNbr = AccountHelper.currentUser.Phone;
            if (order.Car != null)
                model.AssignedCar = order.Car;
            return PartialView("Status", model);
        }

        [HttpPost]
        public JsonResult OrderNote(int id, int type)
        {
            var order = Manager.GetOrderById(id);
            if (order == null)
                throw new Exception(string.Format("Order with id {0} was not found", id));

            var res = Manager.OrderNote(id, (GlobalEnumerator.OrderNoteType)type);

            if (res.IsError)
                this.ShowErrorMsg(res.ErrorMessage);

            this.ShowSuccessMsg("Informacja została przekazana do dyspozytora.");

            return Json(new { result = "OK" });
        }

        [HttpPost]
        public JsonResult CancelOrder(int id)
        {
            var order = Manager.GetOrderById(id);
            if (order == null)
                throw new Exception(string.Format("Order with id {0} was not found", id));

            var res = Manager.OrderCancel(id, GlobalEnumerator.OrderStatus.Canceled_by_client);

            if (res.IsError)
                this.ShowErrorMsg(res.ErrorMessage);

            return Json(new { result = "OK" });
        }

        [HttpPost]
        public JsonResult CheckStatus(int id)
        {
            var order = Manager.GetOrderById(id);
            if (order == null)
                throw new Exception(string.Format("Order with id {0} was not found", id));

            return Json(new { result = "OK", status = order.Status });
        }

        [HttpPost]
        public JsonResult SendCode(string phone)
        {
            //sprawdzam telefon
            if (!new Regex(@"^\d{9}$").IsMatch(phone))
                return Json(new { result = "ERROR", error = "phone" });

            var res = ManagerClient.AddByCode(phone);
            if (res.IsError)
                return Json(new { result = "ERROR", error = res.ErrorMessage });

            return Json(new { result = "OK" });
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

            ManagerClient.UpdateLastLoginDate(client.ClientId);

            //Nadpisuje cookie dla przechowywania dodatkowych informacji
            Response.SetAuthCookie(client.Phone, true, userData);
        }

        [HttpPost]
        public JsonResult ConfirmCode(string phone, string code)
        {

            if (ManagerClient.ActivateNewUserByPhone(phone, code))
            {
                var user = ManagerClient.GetClientByPhone(phone);

                AccountHelper.Logout();

                LogInUser(user);

                return Json(new { result = "OK" });
            }

            return Json(new { result = "ERROR" });

        }

        protected override void Dispose(bool disposing)
        {
            if (_manager != null)
                _manager.Dispose();

            if (_managerClient != null)
                _managerClient.Dispose();

            base.Dispose(disposing);
        }



    }
}
