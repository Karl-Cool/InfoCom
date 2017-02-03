using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using NHibernate.Linq;

namespace InfoCom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            var model = new UserIndexViewmodel { Users = UserRepository.get() };

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }


        // Function to register a new user to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterViewModel model)
        {
            if (DbConnect.SessionFactory.OpenSession().Query<User>().Any(u => u.Username == model.Username))
                ModelState.AddModelError("Username", "Username must be unique");
            else if (DbConnect.SessionFactory.OpenSession().Query<User>().Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email must be unique");
            }

            if (ModelState.IsValid)
            {

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, 10);
                var user = new User();
                user.Password = passwordHash;
                user.Email = model.Email;
                user.Username = model.Username;
                user.Name = model.Name;
                UserRepository.add(user);

                TempData["Message"] = "Profile saved!";
                TempData["Type"] = "alert-success";

                return RedirectToAction("Register", "User");
            }

            ModelState.AddModelError("InvalidRegistration", @"Please fill out the form correctly.");

            return View(model);
        }

        public ActionResult Remove(int id)
        {
            UserRepository.delete(id);

            return RedirectToAction("Index", "User");
        }

        //GET
        public ActionResult Edit(int id)
        {
            var user = DbConnect.SessionFactory.OpenSession().Load<User>(id);
            if (user == null)
                return HttpNotFound();

            var model = new UserEditViewModel
            {
                Id = id,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name
            };

            return View(model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit(UserEditViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                Id = model.Id,
                Username = model.Username,
                Email = model.Email,
                Name = model.Name
            };

            UserRepository.update(user);

            TempData["Message"] = "Settings saved.";
            TempData["Type"] = "alert-success";

            return View(model);
        }
    }
}
