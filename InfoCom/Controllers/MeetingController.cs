using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace InfoCom.Controllers
{
    public class MeetingController : Controller
    {
        public ActionResult Index(int? id)
        {
            int id2 = Convert.ToInt32(id);
            Meeting meeting = MeetingRepository.Get(id2);
            var model = new MeetingViewModel();
            foreach (var time in meeting.Times)
            {
                model.Dates.Add(time.Date);
            }

            List<int> alreadyInvitedUserIds = new List<int>();
            List<Invitation> invitations = InvitationRepository.GetMeeting(id2);
            foreach (Invitation invitation in invitations)
            {
                model.Invitations.Add(invitation);
                alreadyInvitedUserIds.Add(invitation.User.Id);
            }
            ICollection<User> allUsers = UserRepository.Get();
            foreach (User user in allUsers)
            {
                if (!alreadyInvitedUserIds.Contains(user.Id))
                {
                    model.NotInvitedUsers.Add(new SelectListItem
                    {
                        Text = user.Name,
                        Value = user.Id.ToString()
                    });
                }
            }
            model.MeetingId = meeting.Id;
            model.Creator = meeting.Creator;
            model.Title = meeting.Title;
            model.Description = meeting.Description;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddNewInvitation(MeetingViewModel model)
        {
                Invitation newInvitation = new Invitation();
                newInvitation.Meeting = MeetingRepository.Get(model.MeetingId);
                newInvitation.Notified = false;
                newInvitation.User = UserRepository.Get(model.UserId);
                newInvitation.Status = 0;
                InvitationRepository.Add(newInvitation);
                return RedirectToAction("Index", new { id = model.MeetingId });
        }


        public ActionResult Create()
        {
            var model = new MeetingCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MeetingCreateViewModel model)
        {
            model.Creator = UserRepository.Get(Convert.ToInt32(User.Identity.GetUserId()));
            if (ModelState.IsValid)
            {
                bool timesAdded = false;
                Meeting meeting = new Meeting();
                meeting.Creator = model.Creator;
                meeting.Description = model.Description;
                meeting.Title = model.Title;
                meeting.Invitations = new HashSet<Invitation>();
                meeting.Times = new HashSet<Time>();
                int id = MeetingRepository.Add(meeting);
                if (id != 0)
                {
                    foreach (string stringDate in model.Dates)
                    {
                        Time newDate = new Time();
                        DateTime dateAndTime = new DateTime();
                        if (DateTime.TryParse(stringDate, out dateAndTime))
                        {
                            newDate.Date = dateAndTime;
                            newDate.Meeting = MeetingRepository.Get(id);
                            if (TimeRepository.add(newDate) != 0)
                            {
                                timesAdded = true;
                            }
                        }

                    }
                    if (timesAdded)
                    {
                        return RedirectToAction("index", new { id = id });
                    }

                }

            }
            return View();
        }
    }
}