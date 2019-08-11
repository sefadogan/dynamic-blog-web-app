using BlogApp.BLL.Abstract;
using BlogApp.DAL.Entity;
using BlogApp.UI.Areas.AdminPanel.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [CustomAuthorization]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public ActionResult List(int page = 1)
        {
            var commentList = _commentService.GetList().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(commentList);
        }
        public ActionResult Edit(int id)
        {
            List<SelectListItem> statusList = new List<SelectListItem>();

            Session["SelectedCommentId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var comment = _commentService.Get(c => c.CommentId == id);
            if (comment == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the comment.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            // Must refactor here.
            statusList.Add(new SelectListItem
            {
                Text = "Passive",
                Value = "0"
            });
            statusList.Add(new SelectListItem
            {
                Text = "Active",
                Value = "1"
            });
            statusList.Add(new SelectListItem
            {
                Text = "Pending",
                Value = "2"
            });

            ViewData["StatusList"] = statusList;
            return View(comment);
        }
        public ActionResult Detail(int id)
        {
            var comment = _commentService.Get(c => c.CommentId == id);
            if (comment == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the comment.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            return View(comment);
        }
        public ActionResult Delete(int id)
        {
            var result = _commentService.Delete(id);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while deleting the comment.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Comment delete process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Comment model)
        {
            int convertedCommentId = Convert.ToInt32(Session["SelectedCommentId"]); // "Get Method" is taking a paramater as expression. So i converted session id then compared them.
            var comment = _commentService.Get(c => c.CommentId == convertedCommentId);

            comment.Status = model.Status;
            comment.Text = model.Text;

            var result = _commentService.Update(comment);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while updating the comment.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Comment update process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }
    }
}