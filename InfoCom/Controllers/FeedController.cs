using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

            PostViewModel model = new PostViewModel();
            model.PostList = PostRepository.Get();
            model.Categories = CategoryRepository.Get().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(PostViewModel model)
        {
            var category = model.CategoryId;
            model.PostList = PostRepository.GetCat(category);
            model.Categories = CategoryRepository.Get().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
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
            try
            {

                if (ModelState.IsValid)
                {
                    var post = new Post();
                    var currentuser = UserRepository.Get(Convert.ToInt32(User.Identity.GetUserId()));
                    post.Author = currentuser;
                    post.Category = CategoryRepository.Get(4);

                    post.Content = model.Content;
                    post.CreatedAt = DateTime.Now;
                    post.Formal = false;
                    post.Title = model.Title;

                    PostRepository.Add(post);
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(model);
                }



            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return HttpNotFound();
            }
            
        }
    }
}