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
        public virtual List<string> Dates { get; set; }
        public virtual List<Invitation> Invitations { get; set; }
        public User Creator { get; set; }

        public MeetingViewModel()
        {
            Dates = new List<string>();
            Invitations = new List<Invitation>();
        }
    }
}