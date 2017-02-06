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
        // GET: Meeting
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                Meeting meeting = MeetingRepository.Get(Convert.ToInt32(id));
                MeetingViewModel viewModel = new MeetingViewModel();
                viewModel.Title = meeting.Title;
                viewModel.Description = meeting.Description;
                viewModel.Creator = UserRepository.Get(meeting.Creator.Id);
                return View("Index", viewModel);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Create()
        {
            MeetingViewModel meetingViewModel = new MeetingViewModel();
            return View(meetingViewModel);
        }

        [HttpPost]
        public ActionResult Create(MeetingViewModel model)
        {
            if (ModelState.IsValid)
            {
                Meeting meeting = new Meeting();
                meeting.Creator = UserRepository.Get(Convert.ToInt32(User.Identity.GetUserId()));
                meeting.Description = model.Description;
                meeting.Title = model.Title;
                int id = MeetingRepository.Add(meeting);
                if (id != 0)
                {
                    foreach (string stringDate in model.Dates)
                    {
                        Time newDate = new Time();
                        DateTime dateAndTime = new DateTime();
                        DateTime.TryParse(stringDate, out dateAndTime);
                        newDate.Date = dateAndTime;
                        newDate.Meeting = MeetingRepository.Get(id);
                        meeting.Times.Add(newDate);
                    }

                    return RedirectToAction("info", id);
                }

            }
            return View();
        }
    }
}