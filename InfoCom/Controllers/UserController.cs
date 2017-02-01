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
        public ActionResult Register()
        {
            return View();
        }


        // Function to register a new user to the database
        [HttpPost]
        [AllowAnonymous]
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
            }
            
            else
            {
                ModelState.AddModelError("InvalidRegistration", @"Please fill out the form correctly.");
            }
            return View();
        }
    }
}
