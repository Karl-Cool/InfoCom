using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfoCom.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
           //var currentFeed = FeedRepository.get(1);
            return View();
        }
    }
}