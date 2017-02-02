using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoCom.ViewModels
{
    public class FeedViewModel
    {
        public List<Post> PostList { get; set; }

        public FeedViewModel()
        {
            PostList = new List<Post>();
        }
    }
}