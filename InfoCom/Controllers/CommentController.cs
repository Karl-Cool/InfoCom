using System;
using System.Web.Mvc;
using DataAccess.Models;
using DataAccess.Repositories;
using InfoCom.ViewModels;

namespace InfoCom.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        [HttpPost]
        public ActionResult Create(CommentViewModel model)
        {
            var comment = new Comment
            {
                Author = model.Author,
                Content = model.Content,
                CreatedAt = DateTime.Now,
                Post = model.Post,
                Inactive = false
            };
            CommentRepository.Create(comment);
            return RedirectToAction("Read/" + model.Post.Id, "Post");
        }

        public ActionResult Delete(int id, int postId)
        {
            CommentRepository.Deactivate(id);
            return RedirectToAction("Read", "Post", new { id = postId});
        }
    }
}