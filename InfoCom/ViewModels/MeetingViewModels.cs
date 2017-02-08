using System.ComponentModel.DataAnnotations;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace InfoCom.ViewModels
{
    public class MeetingViewModel
    {
        [Required(ErrorMessage = "Enter a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter a description")]
        public string Description { get; set; }
        public virtual List<Time> Times { get; set; }
        public virtual List<Invitation> Invitations { get; set; }
        public virtual List<Invitation> StatusZero { get; set; }
        public virtual List<Invitation> StatusOne { get; set; }
        public virtual List<Invitation> StatusTwo { get; set; }
        public virtual Invitation CurrentUsersInvitation { get; set; }
        public virtual List<SelectListItem> NotInvitedUsers { get; set; }
        public virtual List<int> AlreadySelectedTimes { get; set; }
        public virtual User Creator { get; set; }
        public virtual int UserId { get; set; }
        public virtual int MeetingId { get; set; }
        public virtual int CurrentUserId { get; set; }
        public virtual bool Invited { get; set; }
        public virtual bool Inactive { get; set; }
        public virtual int[] VoteArray { get; set; }
        public virtual DateTime? ConfirmedTime { get; set;}

        public MeetingViewModel()
        {
            Times = new List<Time>();
            Invitations = new List<Invitation>();
            NotInvitedUsers = new List<SelectListItem>();
            StatusZero = new List<Invitation>();
            StatusOne = new List<Invitation>();
            StatusTwo = new List<Invitation>();
        }
    }
   
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

    public class MeetingCreateViewModel
    {
        [Required(ErrorMessage = "Enter a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Enter a description")]
        public string Description { get; set; }
        public virtual List<string> Dates { get; set; }
        public User Creator { get; set; }

        public MeetingCreateViewModel()
        {
            Dates = new List<string>();
        }
    }
}