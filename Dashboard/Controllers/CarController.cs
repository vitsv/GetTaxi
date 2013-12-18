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
    public class CarController : Controller
    {

        private CarManager _manager;
        private CarManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = new CarManager();

                return _manager;
            }
        }

        public ActionResult Index()
        {
            var model = Manager.GetAll();
            return View(model);
        }

        public ActionResult CompanyCars(int companyId)
        {
            var company = Manager.RepoGeneric.FindOne<Company>(c => c.CompanyId == companyId);

            if (company == null)
                throw new Exception("Company doesn't found");

            ViewBag.CompanyId = companyId;
            ViewBag.CompanyName = company.Name;

            var model = Manager.GetForCompany(companyId);

            return View(model);
        }

        public ActionResult Add(int companyId)
        {
            var company = Manager.RepoGeneric.FindOne<Company>(c => c.CompanyId == companyId);

            if (company == null)
                throw new Exception("Company doesn't found");

            var model = new EditableCar();
            model.CompanyName = company.Name;
            model.CompanyId = company.CompanyId;

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(EditableCar modelCar)
        {
            if (ModelState.IsValid)
            {
                if (modelCar != null)
                {
                    Car car = new Car();
                    car.Mark = modelCar.Mark;
                    car.Model = modelCar.Model;
                    car.Color = modelCar.Color;
                    car.NrOfSeats = modelCar.NrOfSeats;
                    car.CarNumber = modelCar.CarNumber;
                    car.DriverName = modelCar.DriverName;
                    car.CompanyId = modelCar.CompanyId;

                    Manager.Add(car);


                    return RedirectToAction("Index");
                }
            }

            var company = Manager.RepoGeneric.FindOne<Company>(c => c.CompanyId == modelCar.CompanyId);

            if (company == null)
                throw new Exception("Company doesn't found");

            modelCar.CompanyName = company.Name;
            modelCar.CompanyId = company.CompanyId;

            return View(modelCar);
        }

        public ActionResult Edit(int id)
        {
            var car = Manager.GetById(id);

            EditableCar model = new EditableCar();
            model.CarId = car.CarId;
            model.Mark = car.Mark;
            model.Model = car.Model;
            model.Color = car.Color;
            model.NrOfSeats = car.NrOfSeats;
            model.CarNumber = car.CarNumber;
            model.DriverName = car.DriverName;
            model.CompanyId = car.CompanyId;
            model.CompanyName = car.Company.Name;

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(EditableCar modelCar)
        {
            if (ModelState.IsValid)
            {
                Car car = new Car();
                car.CarId = modelCar.CarId.GetValueOrDefault();
                car.Mark = modelCar.Mark;
                car.Model = modelCar.Model;
                car.Color = modelCar.Color;
                car.NrOfSeats = modelCar.NrOfSeats;
                car.CarNumber = modelCar.CarNumber;
                car.DriverName = modelCar.DriverName;
                car.CompanyId = modelCar.CompanyId;

                Manager.Edit(car);

                return RedirectToAction("Index");
            }
            else
            {
                return View(modelCar);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var res = Manager.Delete(id);

            if (!res.IsError)
                return Json(new { result = "OK" });

            return Json(new { result = "ERROR", msg = res.ErrorMessage });
        }


    }
}
