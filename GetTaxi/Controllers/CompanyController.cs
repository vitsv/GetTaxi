using Data.Domain;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class CompanyController : Controller
    {

        private CompanyManager _manager;
        private CompanyManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = new CompanyManager();

                return _manager;
            }
        }

        public ActionResult Index()
        {
            var companies = Manager.GetAllCompanies();
            return View(companies);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Company model)
        {
            if (ModelState.IsValid)
            {
                Manager.AddCompany(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = Manager.GetById(id);

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(int id, Company model)
        {
            if (ModelState.IsValid)
            {
                Manager.EditCompany(model);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var res = Manager.DeleteCompany(id);

            if (!res.IsError)
                return Json(new { result = "OK" });

            return Json(new { result = "ERROR", msg = res.ErrorMessage });
        }


    }
}
