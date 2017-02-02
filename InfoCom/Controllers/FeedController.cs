using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfoCom.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
            var model = new FeedViewModel() {
                PostList = PostRepository.get()
            };
           
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Post post)
        {

            post = new Post();
            post.Author.Name = User.Identity.Name; 
            post.Category.Name = "";
            
            post.Content = "";
            post.CreatedAt = "";
            post.Formal = false;
            post.Title = "";

            PostRepository.add(post);
            return View();
        }
    }
}