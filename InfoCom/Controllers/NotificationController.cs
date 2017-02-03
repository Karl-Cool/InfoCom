using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using DataAccess.Repositories;

namespace InfoCom.Controllers
{
    public class NotificationController : ApiController
    {
        // Function to get the meeting invitation count for dynamic updating notifications
        public IHttpActionResult GetNotifications()
        {
            int count = 0;
            try
            {
                var invitations = InvitationRepository.get(Convert.ToInt32(User.Identity.GetUserId()));
                foreach (var x in invitations)
                {
                    if (x.Notified == false)
                    {
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Json(count);
        }
    }
}