using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DataAccess.Models;
using DataAccess.Repositories;

namespace InfoCom.Controllers
{
    public class CalendarController : ApiController
    {
        //GET: CalendarApi
        public IHttpActionResult get()
        {
            var meetingList = new List<Meeting>();
            meetingList = MeetingRepository.Get();
            

            return Json(meetingList);
        }
    }
}