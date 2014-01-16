using Data.Domain;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dashboard.Models.Car;

namespace Dashboard.Controllers
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

    }
}
