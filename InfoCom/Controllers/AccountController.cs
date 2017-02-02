using DataAccess;
using DataAccess.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using NHibernate.Linq;
using InfoCom.ViewModels;
using System.Web;

namespace InfoCom.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var session = DbConnect.SessionFactory.OpenSession())
            {
                var usernameCheck = session.Query<User>().FirstOrDefault(x => x.Username == model.Username);
                if (usernameCheck == null)
                {
                    TempData["Message"] = "Invalid username or password";
                    TempData["Type"] = "alert-danger";
                    return View(model);
                }
                var passwordCheck =
                    session.Query<User>()
                        .Where(x => x.Username.Equals(model.Username))
                        .Select(x => x.Password)
                        .Single();

                if (BCrypt.Net.BCrypt.Verify(model.Password, passwordCheck))
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                            new Claim(ClaimTypes.NameIdentifier,
                                session.Query<User>()
                                    .Where(x => x.Username.Equals(model.Username))
                                    .Select(x => x.Id)
                                    .Single()
                                    .ToString()),
                            new Claim(ClaimTypes.Name, model.Username)
                        }, "InfoComCookie");

                    if (model.Username.ToLower() == "admin")
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                    }

                    var authManager = Request.GetOwinContext().Authentication;
                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Message"] = "Invalid username or password";
            TempData["Type"] = "alert-danger";
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            var authManager = Request.GetOwinContext().Authentication;
            authManager.SignOut("InfoComCookie");
            return RedirectToAction("Login", "Account");
        }
    }
}