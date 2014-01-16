using Dashboard.Models;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class OrdersController : Controller
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

        public ActionResult Index()
        {
            var orders = Manager.GetOrdersGridModelForCompany(1); //TODO: pobrać id firmy z zalogowanego usera
            return View(orders);
        }

        public JsonResult GetOrders(JQueryDataTableParamModel param)
        {
            var orders = Manager.GetOrdersGridModelForCompany(1); //TODO: pobrać id firmy z zalogowanego usera
            var json = Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = orders.Count(),
                iTotalDisplayRecords = orders.Count(),
                aaData = orders
            });
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
    }
}
