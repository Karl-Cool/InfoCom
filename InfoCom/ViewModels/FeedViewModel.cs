using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace InfoCom.ViewModels
{
    public class FeedViewModel
    {
        public List<Post> PostList { get; set; }
        public User UserID { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Formal { get; set; }
       
        public FeedViewModel()
        {

            PostList = new List<Post>();
        }

    }
}