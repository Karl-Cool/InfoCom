using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DataAccess.Models;
using DataAccess.Repositories;

namespace InfoCom.Controllers
{
    public class CalendarApiController : ApiController
    {
        // GET: CalendarApi
        //public IHttpActionResult GetMeetingInfo()
        //{
        //    var meetingList = new List<Meeting>();
        //    var meetings = MeetingRepository.Get();

        //    foreach (var item in meetings)
        //    {
        //        var meeting = new Meeting
        //        {
        //            Id = item.Id,
        //            Title = item.Title,
        //            Times = MeetingRepository.GetTime(item.Id)
        //        };
        //    }
            
        //    return Json();
        //}
    }
}