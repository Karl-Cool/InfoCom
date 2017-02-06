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
                foreach(DateTime date in model.Dates)
                {
                    Time newDate = new Time();
                    newDate.Date = date;
                    meeting.Times.Add(newDate);
                }

                int id = MeetingRepository.Add(meeting);
                if (id != 0)
                {
                    return RedirectToAction("Index", id);
                }
                    //    {
                    //        Meeting newMeeting = MeetingRepository.get(id);
                    //        foreach (Time time in model.Times)
                    //        {
                    //            time.Meeting = newMeeting;
                    //            if (TimeRepository.add(time) != 0)
                    //            {
                    //                return RedirectToAction("Info",id);
                    //            }
                    //        }
                    //    }
                    //}
                }
            return View();
        }
    }
}