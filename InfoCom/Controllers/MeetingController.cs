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
            return View();
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