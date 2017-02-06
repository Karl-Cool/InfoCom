using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Models;

namespace InfoCom.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Time> Times { get; set; }
    }
}