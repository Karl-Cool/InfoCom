using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using Microsoft.AspNet.Identity;
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
            // var model = new FeedViewModel() {
            //     PostList = PostRepository.get()
            //};
            List<Post> model = PostRepository.get();

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Post post)
        {
            var currentuser = UserRepository.get(11);
            // post.Author = User.Identity.Name;
            post.Author = currentuser;

            
            post.Content = "LOL";
            post.CreatedAt = "20/20/20";
            post.Formal = false;
            post.Title = "Småbarn";

            PostRepository.add(post);
            return View();
        }
    }
}