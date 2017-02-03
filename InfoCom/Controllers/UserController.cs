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

        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int id)
        {
            if (UserRepository.delete(id))
            {
                return RedirectToAction("Index", "User");
            }

            return RedirectToAction("Index", "Home");


        }

        //GET
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var user = DbConnect.SessionFactory.OpenSession().Load<User>(id);
            if (user == null)
                return HttpNotFound();

            return View(new UserEditViewModel
                {
                    Username = user.Username,
                    Email = user.Email,
                    Name = user.Name
                });
        }

        //POST
        [HttpPost]
        public ActionResult Edit(int id, UserEditViewModel model)
        {
            var user = DbConnect.SessionFactory.OpenSession().Load<User>(id);
            if (user == null)
                return HttpNotFound();

            if(DbConnect.SessionFactory.OpenSession().Query<User>().Any(u=>u.Username == model.Username && u.Id != id))
                //om en användare hittas i db som inte är modellen
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(model);

            user.Username = model.Username;
            user.Email = model.Email;
            user.Name = model.Name;

       
            //Failar vid update pga 2 öppna sessions. hann inte fixa innan push men allt bygger
           DbConnect.SessionFactory.OpenSession().Update(user);

            return RedirectToAction("Index", "User");
        }
    }
}
