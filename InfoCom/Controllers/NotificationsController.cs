using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using DataAccess.Repositories;

namespace Datingsite.Controllers
{
    public class NotificationsController : ApiController
    {
        // Function to get the meeting invitation count for dynamic updating notifications
        public IHttpActionResult GetNotifications()
        {
            try
            {
                var invitations = InvitationRepository.get(Convert.ToInt32(User.Identity.GetUserId())).Count;

                if (invitations == 0)
                {
                    return Json("0");
                }
                return Json(invitations);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Json("0");
            }
        }
    }
}