using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace InfoCom.Controllers
{
    public class MeetingController : Controller
    {
        // GET: Meeting
        public ActionResult Index()
        {
            List<Meeting> meetings = MeetingRepository.get();
            return View(meetings);
        }
        public ActionResult Info(int? id)
        {
            if (id != null)
            {
                Meeting meeting = MeetingRepository.get(Convert.ToInt32(id));
                MeetingViewModel viewModel = new MeetingViewModel();
                viewModel.Title = meeting.Title;
                viewModel.Description = meeting.Description;
                viewModel.Creator = UserRepository.get(meeting.Creator.Id);
                return View("Info", viewModel);
            }
            return RedirectToAction("Index");
        } 
        

        public ActionResult Create()
        {
            MeetingViewModel meetingViewModel = new MeetingViewModel();
            return View(meetingViewModel);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Title,Description")] Meeting meeting)
        {
            meeting.Creator = UserRepository.get(Convert.ToInt32(User.Identity.GetUserId()));
            if (ModelState.IsValid)
            {
                if (MeetingRepository.add(meeting))
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}