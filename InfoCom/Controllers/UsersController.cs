using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using DataAccess.Repositories;
using System.Net.Http;
using System.Net;
using System.Diagnostics;

namespace InfoCom.Controllers
{
    public class UsersController : ApiController
    {
        //Function to deactivate a user
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            var userData = UserRepository.Deactivate(id);
            var meetingData = MeetingRepository.DeactivateAll(id);

            if (userData && meetingData)
            {
                var newUrl = Url.Link("Default", new
                {
                    Controller = "User",
                    Action = "Index"
                });
                return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Error: Could not deactivate user.");
            }
        }
    }
}