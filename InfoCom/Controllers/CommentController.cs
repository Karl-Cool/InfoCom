using System.Web.Mvc;
using InfoCom.ViewModels;

namespace InfoCom.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CommentViewModel model)
        {
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(CommentViewModel model)
        {
            return View(model);
        }
    }
}