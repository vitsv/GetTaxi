using Data.Domain;
using Data.Enumerators;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Dashboard.Common;
using Dashboard.Infrastructure;
using Dashboard.Models;
using Dashboard.Models.User;

namespace Dashboard.Controllers
{
    public class UserController : Controller
    {
        private UserManager _manager;
        private UserManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = new UserManager();

                return _manager;
            }
        }

        public ActionResult Index()
        {
            var model = Manager.GetAllUsers();
            return View(model);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            var model = new EditableUser();
            model.UserRoles = new List<int>();

            ViewBag.RoleList = Manager.RepoGeneric.GetAll<Role>().ToList();

            return View(model);
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(EditableUser model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User();
                newUser.Login = model.Login;
                newUser.LastName = model.LastName;
                newUser.FirstName = model.FirstName;
                newUser.Email = model.Email;
                newUser.CreationDate = DateTime.Now;
                newUser.Password = model.Password;

                var res = Manager.AddUser(newUser);
                if (!res.IsError)
                    Manager.UpdateRoles(newUser, model.UserRoles);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id)
        {
            var user = Manager.GetUserById(id);

            var model = new EditableUser();

            model.UserId = user.UserId;
            model.Login = user.Login;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;

            model.UserRoles = user.UserRole.Select(c => c.RoleId).ToList();

            ViewBag.RoleList = Manager.RepoGeneric.GetAll<Role>().ToList();

            return View(model);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EditableUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new User();

                user.UserId = model.UserId;
                user.Login = model.Login;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                var res = Manager.EditUser(user);
                if (!res.IsError)
                    Manager.UpdateRoles(user, model.UserRoles);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.RoleList = Manager.RepoGeneric.GetAll<Role>().ToList();
                return View(model);
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
