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
    public class MeetingsController : Controller
    {
        // GET: Meetings
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            var meetings = MeetingsRepository.getMeetingsByUserId(userId);
            var invitations = InvitationRepository.get(userId);
            var model = new MeetingsViewModel()
            {
                Meetings = meetings,
                Invitations = invitations
            };

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            MeetingsRepository.delete(id);

            return RedirectToAction("Index", "Meetings");
        }
    }
}