using System.ComponentModel.DataAnnotations;
using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace InfoCom.ViewModels
{
    public class MeetingViewModel
    {
        [Required(ErrorMessage = "Enter a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter a description")]
        public string Description { get; set; }
        public Time Time1 { get; set; }
        public Time Time2 { get; set; }
        public Time Time3 { get; set; }
        public Time Time4 { get; set; }
        public Time Time5 { get; set; }
        public virtual ISet<Time> Times { get; set; }
        public virtual ISet<Invitation> Invitations { get; set; }
        public User Creator { get; set; }

    }
}