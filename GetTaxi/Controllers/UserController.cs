using Data.Domain;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Translator.Models.User;

namespace Translator.Controllers
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
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(AddUserModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User();
                newUser.Login = model.Login;
                newUser.LastName = model.LastName;
                newUser.FirstName = model.FirstName;
                newUser.Email = model.Email;
                newUser.CreationDate = DateTime.Now;

                Manager.AddUser(newUser);

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

            var role = user.UserRole.FirstOrDefault();

            if (role != null)
                model.RoleId = role.RoleId;

            ViewBag.RoleList = Manager.RepoGeneric.GetAll<Role>().Select(c => new SelectListItem() { Text = c.Name, Value = c.RoleId.ToString() }).ToList();

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

                if (model.RoleId.HasValue)
                    user.UserRole.Add(new UserRole() { RoleId = model.RoleId.Value });

                Manager.EditUser(user);

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.RoleList = Manager.RepoGeneric.GetAll<Role>().Select(c => new SelectListItem() { Text = c.Name, Value = c.RoleId.ToString() }).ToList();
                return View(model);
            }
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
