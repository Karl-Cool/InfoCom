using DataAccess.Models;
using System.Collections.Generic;

namespace InfoCom.ViewModels
{
    public class MeetingsViewModel
    {
        public virtual List<Meeting> Meetings { get; set; }
        public virtual List<Invitation> Invitations { get; set; }


        public MeetingsViewModel()
        {
            Meetings = new List<Meeting>();
            Invitations = new List<Invitation>();
        }
    }
}