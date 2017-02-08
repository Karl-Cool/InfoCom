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
                model.Categories = CategoryRepository.Get().Select(x => new SelectListItem
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
            var category = model.CategoryId;
            model.FileList = FileRepository.Get();
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
        public ActionResult Post(PostViewModel model, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {


                    var fileObj = new PostFile();
                    var fileObj2 = new PostFile();
                    var fileObj3 = new PostFile();
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
                    var path = Path.Combine("~/Uploads/",
                                      post.Author.Id + fileName);
                    fileObj.FilePath = path;
                    
                    file.SaveAs(Server.MapPath(path));
                    
                    post.Files.Add(fileObj);
                    

                }
                if (file2 != null && file2.ContentLength > 0)
                {

                    // extract only the filename
                    var fileName = Path.GetFileName(file2.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine("~/Uploads/",
                                      post.Author.Id + fileName);
                    fileObj2.FilePath = path;

                    file.SaveAs(Server.MapPath(path));

                    post.Files.Add(fileObj2);


                }
                if (file3 != null && file3.ContentLength > 0)
                {

                    // extract only the filename
                    var fileName = Path.GetFileName(file3.FileName);
                    // store the file inside ~/App_Data/uploads folder
                    var path = Path.Combine("~/Uploads/",
                                      post.Author.Id + fileName);
                    fileObj3.FilePath = path;

                    file.SaveAs(Server.MapPath(path));

                    post.Files.Add(fileObj3);


                }

                    post.Content = model.Content;
                    post.CreatedAt = DateTime.Now;
                    post.Formal = model.Formal;
                    post.Title = model.Title;

                    PostRepository.Add(post);
                List<PostFile> listOfNewPost = new List<PostFile>();
                    fileObj.Post = post;
                    fileObj2.Post = post;
                    fileObj3.Post = post;
                    listOfNewPost.Add(fileObj);
                    listOfNewPost.Add(fileObj2);
                    listOfNewPost.Add(fileObj3);
                    FileRepository.AddThree(listOfNewPost);
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