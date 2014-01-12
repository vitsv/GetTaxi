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

        public ActionResult Create()
        {
            PrepareCreate();
            return View();
        }

        private void PrepareCreate()
        {
            ViewBag.CityList = Manager.RepoGeneric.GetAll<City>().OrderBy(c => c.DisplayOrder).Select(c => new SelectListItem { Text = c.Name, Value = c.CityId.ToString() }).ToList();
            List<SelectListItem> hoursList = new List<SelectListItem>();
            for (int i = 1; i <= 24; i++)
            {
                hoursList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = (i == DateTime.Now.Hour) ? true : false });
            }

            List<SelectListItem> minutesList = new List<SelectListItem>();
            for (int i = 0; i <= 60; i = i + 10)
            {
                hoursList.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }

            ViewBag.HoursList = hoursList;
            ViewBag.MinutesList = minutesList;
        }

        [HttpPost]
        public ActionResult Create(EditableOrder model)
        {
            if (ModelState.IsValid)
            {
                Order newOrder = new Order();
                newOrder.UserId = AccountHelper.currentUser.ID;
                newOrder.Status = (int)GlobalEnumerator.OrderStatus.Created;
                newOrder.TimeCreated = DateTime.Now;
                newOrder.Deadline = DateTime.Now.AddMinutes(10);
                newOrder.IsPrepaid = false;

                if (!string.IsNullOrEmpty(model.PlannedOn))
                {
                    newOrder.IsPlanned = true;
                    DateTime planned;
                    if (DateTime.TryParse(model.PlannedOn, out planned))
                        newOrder.PlannedOn = planned;
                    else
                    {
                        ModelState.AddModelError("PlannedOn", "Nie prawidłowy czas");
                        return Create();
                    }
                }

                Address address = new Address();
                address.CityFrom = model.CityFrom;
                address.AddressFrom = model.AddressFrom;
                if (model.CityTo.GetValueOrDefault() > 0)
                    address.CityTo = model.CityTo;
                if (!string.IsNullOrEmpty(model.AddressTo))
                    address.AddressTo = model.AddressTo;
                newOrder.Address = address;

                OrderProperties properties = new OrderProperties();
                if (model.Company > 0)
                    properties.CompanyId = model.Company;
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
                    return RedirectToAction("Status");
                else
                    ViewData["ErrorMsg"] = res.ErrorMessage;
            }

            return Create();
        }

        public ActionResult Status()
        {
            var model = new OrderStatusModel();

            return View(model);
        }

    }
}
