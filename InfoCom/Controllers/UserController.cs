using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.Services;
using InfoCom.ViewModels;
using NHibernate.Linq;

namespace InfoCom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            var model = new UserIndexViewmodel { Users = UserRepository.Get() };

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
                var user = new User
                {
                    Password = passwordHash,
                    Email = model.Email,
                    Username = model.Username,
                    Name = model.Name
                };
                UserRepository.Add(user);

                TempData["Message"] = "Profile saved!";
                TempData["Type"] = "alert-success";

                return RedirectToAction("Register", "User");
            }

            ModelState.AddModelError("InvalidRegistration", @"Please fill out the form correctly.");

            return View(model);
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

            UserRepository.Update(user);

            TempData["Message"] = "Settings saved.";
            TempData["Type"] = "alert-success";

            return View(model);
        }

        public async Task<ActionResult> ResetPassword(int id)
        {
            var password = Membership.GeneratePassword(8, 1);
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password, 10);

            var user = UserRepository.Get(id);
            user.Password = passwordHash;
            UserRepository.UpdatePassword(user);

            var email = new Email
            {
                Recipient = user.Email,
                Title = "Your password has been reset.",
                Text = "Your password on InfoCom has been reset. This is your new password: " + password
            };
            await MailService.Mail(email);

            TempData["Message"] = "The password for " + user.Username + " has been successfully reset.";
            TempData["Type"] = "alert-success";

            return RedirectToAction("Index", "User");
        }
    }
}
