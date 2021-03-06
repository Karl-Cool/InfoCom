﻿using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using DataAccess.Repositories;
using System.Net.Http;
using System.Net;
using System.Diagnostics;

namespace InfoCom.Controllers
{
    public class MeetingsController : ApiController
    {
        //Function to deactivate a user
        [HttpDelete]
        public HttpResponseMessage DeleteMeeting(int id)
        {
            var meetingData = MeetingRepository.Deactivate(id);

            if (meetingData)
            {
                var newUrl = Url.Link("Default", new
                {
                    Controller = "Meeting",
                    Action = "Index"
                });
                return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Error: Could not deactivate meeting.");
            }
        }
    }
}