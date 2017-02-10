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
using System.Web.Mvc;
using FluentNHibernate.Utils;

namespace InfoCom.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            var model = new PostViewModel();
            model.Categories = CategoryRepository.Get().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(model);
        }
        public ActionResult AddCategory(PostViewModel model)
        {
            if (model.NewCategory != "" || model.NewCategory != null)
            {
                var categorymodel = new Category();
                categorymodel.Name = model.NewCategory;
                CategoryRepository.Add(categorymodel);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Post(PostViewModel model, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            try
            {
                model.Categories = CategoryRepository.Get().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                if (ModelState.IsValid)
                {

                    var fileObj = new PostFile();
                    var fileObj2 = new PostFile();
                    var fileObj3 = new PostFile();
                    var post = new Post();
                    var currentuser = UserRepository.Get(Convert.ToInt32(User.Identity.GetUserId()));
                    post.Author = currentuser;
                    
                    var allPosts = PostRepository.Get();
                    int numberOfPosts = allPosts.Count;
                    post.Category = CategoryRepository.Get(model.CategoryId);
                    
                    if (file != null && file.ContentLength > 0)
                    {

                        // extract only the filename
                        var fileName = Path.GetFileName(file.FileName);
                        // store the file inside ~/App_Data/uploads folder
                        var path = Path.Combine("~/Uploads/",
                                          numberOfPosts + fileName);
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
                                          numberOfPosts + fileName);
                        fileObj2.FilePath = path;

                        file2.SaveAs(Server.MapPath(path));

                        post.Files.Add(fileObj2);


                    }
                    if (file3 != null && file3.ContentLength > 0)
                    {

                        // extract only the filename
                        var fileName = Path.GetFileName(file3.FileName);
                        // store the file inside ~/App_Data/uploads folder
                        var path = Path.Combine("~/Uploads/",
                                          numberOfPosts + fileName);
                        fileObj3.FilePath = path;

                        file3.SaveAs(Server.MapPath(path));

                        post.Files.Add(fileObj3);


                    }

                    post.Content = model.Content;
                    post.CreatedAt = DateTime.Now;

                    if (model.Formal == "Formal")
                    {
                        post.Formal = true;
                    }
                    else if (model.Formal == "Informal")
                    {
                        post.Formal = false;
                    }
                    
                    post.Title = model.Title;
                    if (!post.Equals(null))
                    {
                        PostRepository.Add(post);
                    }
                    
                    List<PostFile> listOfNewPost = new List<PostFile>();
                    fileObj.Post = post;
                    fileObj2.Post = post;
                    fileObj3.Post = post;
                    listOfNewPost.Add(fileObj);
                    listOfNewPost.Add(fileObj2);
                    listOfNewPost.Add(fileObj3);
                    if(listOfNewPost != null || listOfNewPost.Count > 0)
                    {
                        FileRepository.AddThree(listOfNewPost);
                    }
                    
                    
                    return RedirectToAction("Index", "Feed");

                  
                }
                else
                {
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }

        }

        public ActionResult Read(int id)
        {
            var model = new ReadViewModel
            {
                Post = PostRepository.Get(id)
               
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Read(ReadViewModel model)
        {
            var comment = new Comment
            {
                Post = PostRepository.Get(model.Id),
                Author = UserRepository.Get(Convert.ToInt32(User.Identity.GetUserId())),
                Content = model.Content,
                CreatedAt = DateTime.Now
            };

            CommentRepository.Create(comment);
            return RedirectToAction("Read/"+model.Id, "Post");
        }


        public ActionResult ToggleVisibility(int id)
        {
            var post = PostRepository.Get(id);
            if (post.Author.Username == User.Identity.Name || User.IsInRole("Admin"))
            {
                if (!post.Inactive)
                {
                    PostRepository.Deactivate(id);
                }
                else if (post.Inactive)
                {
                    PostRepository.Activate(id);
                }
            }
            
            return RedirectToAction("Index", "Feed");
        }
    }
}