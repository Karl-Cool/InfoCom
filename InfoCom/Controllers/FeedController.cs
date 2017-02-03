using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using Microsoft.AspNet.Identity;
using NHibernate.Linq;
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
        public ActionResult Post()
        {
            var model = new PostViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Post(PostViewModel model)
        {
            var session = DbConnect.SessionFactory.OpenSession();
            var usercheck = session.Query<User>().FirstOrDefault(x => x.Id == Convert.ToInt32(User.Identity.GetUserId()));
            var post = new Post();
            var currentuser = UserRepository.get(usercheck.Id);
            // post.Author = User.Identity.Name;
            post.Author = currentuser;
            post.Category = CategoryRepository.get(4);

            post.Content = model.Content;
            post.CreatedAt = DateTime.Now.ToString();
            post.Formal = false;
            post.Title = "Exempeltitel";

            PostRepository.add(post);
            return View(model);
        }
    }
}