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
    public class UserController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = new UserIndexViewmodel();
            model.Users = UserRepository.get();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Register()
        {
            return View();
        }


        // Function to register a new user to the database
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel model)
        {
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

        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int id)
        {
            if (UserRepository.delete(id))
            {
                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Index", "Home");


        }
    }
}
