using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace InfoCom.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
            try
            {
                var model = new PostViewModel();
                model.PostList = PostRepository.Get();
                var categoryList = CategoryRepository.Get().ToList();
                var all = new Category
                {
                    Id = 0,
                    Name = "All"
                };
                categoryList.Insert(0, all);
                model.Categories = categoryList.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
               
                model.FileList = FileRepository.Get();

                return View(model);
            }
            // var model = new FeedViewModel() {
            //     PostList = PostRepository.get()
            //};
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return HttpNotFound();
            }

            
        }


        [HttpPost]
        public ActionResult Index(PostViewModel model)
        {
            model.FileList = FileRepository.Get();
            model.PostList = PostRepository.GetCat(model.CategoryId, model.Formal);
            var categoryList = CategoryRepository.Get().ToList();
            var all = new Category
            {
                Id = 0,
                Name = "All"
            };
            categoryList.Insert(0, all);
            model.Categories = categoryList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });


            return View(model);
        }
      

        
    }
}