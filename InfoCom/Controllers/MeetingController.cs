using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Title,Description,Creator")] Meeting meeting)
        {
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