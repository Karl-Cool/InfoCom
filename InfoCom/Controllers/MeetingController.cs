using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using InfoCom.Services;
using System.Threading.Tasks;

namespace InfoCom.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        // GET: Meetings
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            var meetings = MeetingsRepository.Get(userId);
            meetings.Reverse();
            var invitations = InvitationRepository.Get(userId);
            invitations.Reverse();
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

        public ActionResult Profile(int? id)
        {
            try
            {
                int id2 = Convert.ToInt32(id);
                Meeting meeting = MeetingRepository.Get(id2);
                var model = new MeetingViewModel();
                if (meeting.Times.Count > 0)
                {
                    foreach (var time in meeting.Times)
                    {
                        model.Times.Add(time);
                    }
                }
                List<int> alreadyInvitedUserIds = new List<int>();
                List<Invitation> StatusZero = new List<Invitation>();
                List<Invitation> StatusOne = new List<Invitation>();
                List<Invitation> StatusTwo = new List<Invitation>();
                List<Invitation> invitations = InvitationRepository.GetMeeting(id2);
                if (invitations.Count > 0)
                {
                    foreach (Invitation invitation in invitations)
                    {
                        model.Invitations.Add(invitation);
                        alreadyInvitedUserIds.Add(invitation.User.Id);
                        if (invitation.User.Id == Convert.ToInt32(User.Identity.GetUserId()))
                        {
                            model.CurrentUsersInvitation = invitation;
                        }
                        if (invitation.Status == 0)
                        {
                            StatusZero.Add(invitation);
                        }
                        if (invitation.Status == 1)
                        {
                            StatusOne.Add(invitation);
                        }
                        if (invitation.Status == 2)
                        {
                            StatusTwo.Add(invitation);
                        }
                    }
                }
                ICollection<User> allUsers = UserRepository.Get();
                foreach (User user in allUsers)
                {
                    if (!alreadyInvitedUserIds.Contains(user.Id) && user.Id != meeting.Creator.Id && allUsers.Count > 0)
                    {
                        model.NotInvitedUsers.Add(new SelectListItem
                        {
                            Text = user.Name,
                            Value = user.Id.ToString()
                        });
                    }
                }
                var allTimeChoices = TimeChoiceRepository.Get();
                var currentUsersCoices = new List<int>();

                foreach (var choice in allTimeChoices)
                {
                    if (choice.User.Id == Convert.ToInt32(User.Identity.GetUserId()))
                    {
                        currentUsersCoices.Add(choice.Time.Id);
                    }
                }
                if (alreadyInvitedUserIds.Contains(Convert.ToInt32(User.Identity.GetUserId())))
                {
                    model.Invited = true;
                }
                else
                {
                    model.Invited = false;
                }
                var voteList = new List<int>();
                foreach (var time in meeting.Times)
                {
                    voteList.Add(TimeChoiceRepository.GetCount(time.Id));
                }
                model.VoteArray = voteList.ToArray();
                model.Inactive = meeting.Inactive;
                model.ConfirmedTime = meeting.ConfirmedTime;
                model.AlreadySelectedTimes = currentUsersCoices;
                model.CurrentUserId = Convert.ToInt32(User.Identity.GetUserId());
                model.MeetingId = meeting.Id;
                model.Creator = meeting.Creator;
                model.Title = meeting.Title;
                model.Description = meeting.Description;
                model.StatusZero = StatusZero;
                model.StatusOne = StatusOne;
                model.StatusTwo = StatusTwo;
                return View(model);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
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
            return RedirectToAction("Profile", new { id = model.MeetingId });
        }
        public ActionResult AddNewTimeChoice(int id)
        {
            User currentUser = UserRepository.Get(Convert.ToInt32(User.Identity.GetUserId()));
            Time timeChosen = TimeRepository.Get(id);
            TimeChoice newTimeChoice = new TimeChoice();
            newTimeChoice.Time = timeChosen;
            newTimeChoice.User = currentUser;
            newTimeChoice.Meeting = timeChosen.Meeting;
            TimeChoiceRepository.Add(newTimeChoice);
            updateInvitationStatus(currentUser, timeChosen.Meeting.Id, 1);

            return RedirectToAction("Profile", new { id = newTimeChoice.Meeting.Id });
        }

        public ActionResult DeclineInvitation(int id)
        {
            User currentUser = UserRepository.Get(Convert.ToInt32(User.Identity.GetUserId()));
            updateInvitationStatus(currentUser, id, 2);
            return RedirectToAction("Profile", new { id = id });
        }

        private void updateInvitationStatus(User currentUser, int meetingId, int newStatus)
        {
            Invitation updatedInvite = new Invitation();
            foreach (var invitation in InvitationRepository.Get(currentUser.Id))
            {
                if (invitation.Meeting.Id == meetingId)
                {
                    updatedInvite = invitation;
                }
            }
            updatedInvite.Status = newStatus;
            InvitationRepository.Update(updatedInvite);
        }
        public async Task<ActionResult> AddConfirmedTime(int id)
        {
            Time timeChosen = TimeRepository.Get(id);
            Meeting meeting = MeetingRepository.Get(timeChosen.Meeting.Id);
            meeting.ConfirmedTime = timeChosen.Date;
            MeetingRepository.Update(meeting);
            await sendMeetingUpdateMail(meeting);
            return RedirectToAction("Profile", new { id = meeting.Id });
        }

        private async Task sendMeetingUpdateMail(Meeting meeting)
        {
            List<Invitation> invitations = InvitationRepository.GetMeeting(meeting.Id);
            if (invitations.Count > 0)
            {
                foreach (Invitation invitation in invitations)
                {
                    if (invitation.Status == 1)
                    {
                        Email mail = new Email();
                        mail.Title = "Meeting: " + meeting.Title + " have been updated!";
                        mail.Text = "The time for the meeting " + meeting.Title + " has been finalized.\n The new time is " + meeting.ConfirmedTime.ToString() + ".";
                        mail.Recipient = invitation.User.Email;
                        await MailService.Mail(mail);
                    }
                }
            }
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
                        return RedirectToAction("Profile", new { id = id });
                    }
                }
            }
            return View();
        }
    }
}