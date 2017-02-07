using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.Models;

namespace InfoCom.Controllers
{
    public class CalendarController : ApiController
    {
        //GET: CalendarApi
        public IHttpActionResult Get()
        {
            var meetingList = MeetingRepository.Get();
            var eventList = meetingList.Select(item => new Event
            {
                Id = item.Id,
                Title = item.Title,
                ConfirmedTime = item.ConfirmedTime
            }).ToList();


            return Json(eventList);
        }
    }
}