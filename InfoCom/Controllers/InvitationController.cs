using DataAccess.Repositories;
using InfoCom.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace InfoCom.Controllers
{
    public class InvitationController : Controller
    {
        // GET: Invitation
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            var invitations = InvitationRepository.get(userId);
            var model = new InvitationViewModel()
            {
                Invitations = invitations
            };
            if (invitations != null)
            {
                InvitationRepository.updateNotified(userId);
            }
            return View(model);
        }
    }
}