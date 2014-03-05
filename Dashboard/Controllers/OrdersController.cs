using Dashboard.Common;
using Dashboard.Models;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    [Authorize]
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
            
            var orders = Manager.GetOrdersGridModelForCompany(GetCurrentCompanyId()); //TODO: pobrać id firmy z zalogowanego usera
            return View(orders);
        }

        public JsonResult GetOrders(JQueryDataTableParamModel param)
        {
            var orders = Manager.GetOrdersGridModelForCompany(GetCurrentCompanyId()); //TODO: pobrać id firmy z zalogowanego usera
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

        public JsonResult GetOrderDetailsJson(int id)
        {
            var order = Manager.GetOrderDetails(id);

            var json = Json(order);
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public ActionResult GetOrderDetails(int id)
        {
            try
            {
                var order = Manager.GetOrderDetails(id);


                return PartialView("_OrderDetails", order);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private int GetCurrentCompanyId()
        {
            var userID = AccountHelper.currentUser.ID;
            UserManager userManager = new UserManager();
            var user = userManager.GetUserById(userID);
            return (int)user.CompanyId;
        }
    }
}
