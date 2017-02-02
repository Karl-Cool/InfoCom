using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoCom.ViewModels
{
    public class InvitationViewModel
    {
        public virtual List<Invitation> Invitations { get; set; }

        public InvitationViewModel()
        {
            Invitations = new List<Invitation>();
        }
    }
}