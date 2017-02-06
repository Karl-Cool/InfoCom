using DataAccess.Repositories;
using InfoCom.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace InfoCom.Controllers
{
    public class MeetingsController : Controller
    {
        // GET: Meetings
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            var meetings = MeetingsRepository.Get(userId);
            var invitations = InvitationRepository.Get(userId);
            if (invitations != null)
            {
                InvitationRepository.UpdateNotified(userId);
            }
            var model = new MeetingsViewModel()
            {
                Meetings = meetings,
                Invitations = invitations
            };

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            MeetingsRepository.Delete(id);

            return RedirectToAction("Index", "Meetings");
        }
    }
}