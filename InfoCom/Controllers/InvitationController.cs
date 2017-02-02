using DataAccess.Repositories;
using InfoCom.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfoCom.Controllers
{
    public class InvitationController : Controller
    {
        // GET: Invitation
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            var model = new InvitationViewModel()
            {
                Invitations = InvitationRepository.get(userId)
            };

            return View(model);
        }
    }
}