using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfoCom.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        // GET: Feed
        public ActionResult Index()
        {
            // var model = new FeedViewModel() {
            //     PostList = PostRepository.get()
            //};

            var model = new PostViewModel();
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
            model.Categories = CategoryRepository.Get().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(model);
        }

        [HttpPost]
        public ActionResult Post(PostViewModel model, HttpPostedFileBase file)
        {
            try
            {


                    var fileObj = new PostFile();
                    var post = new Post();
                    var currentuser = UserRepository.Get(Convert.ToInt32(User.Identity.GetUserId()));
                    post.Author = currentuser;
                    
                    post.Category = CategoryRepository.Get(model.CategoryId);
                    if(model.Category.Name != null)
                {
                    var categorymodel = new Category();
                    categorymodel.Name = model.Category.Name;
                    CategoryRepository.Add(categorymodel);
                    return RedirectToAction("Post");
                }
                if (file != null && file.ContentLength > 0)
                {
                    
                    // extract only the filename
                    var fileName = Path.GetFileName(file.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    fileObj.FilePath = path;
                    
                    file.SaveAs(path);
                    
                    post.Files.Add(fileObj);
                    

                }

                    post.Content = model.Content;
                    post.CreatedAt = DateTime.Now;
                    post.Formal = model.Formal;
                    post.Title = model.Title;

                    PostRepository.Add(post);
                    fileObj.Post = post;
                    FileRepository.Add(fileObj);
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return HttpNotFound();
            }
            
        }
      
    }
}