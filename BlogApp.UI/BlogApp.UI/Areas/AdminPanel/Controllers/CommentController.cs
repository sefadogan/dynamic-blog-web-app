using BlogApp.BLL.Services;
using BlogApp.DAL.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [Filters.AutorizeUser]
    public class CommentController : Controller
    {
        public ActionResult List(int page = 1)
        {
            var commentList = new CommentService().ListAll().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(commentList);
        }
        public ActionResult Edit(int id)
        {
            List<SelectListItem> statusList = new List<SelectListItem>();

            Session["SelectedCommentId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var comment = new CommentService().BringById(id);

            if (comment != null)
            {
                // Burası daha sonra düzeltilmeli. (Enum)
                statusList.Add(new SelectListItem {
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
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the comment.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Detail(int id)
        {
            var comment = new CommentService().BringById(id);

            if (comment != null)
            {
                return View(comment);
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the comment.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Delete(int id)
        {
            var result = new CommentService().Delete(id);

            if (result)
            {
                TempData["ServiceResult"] = "Comment delete process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while deleting the comment.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public ActionResult Edit(Comment model)
        {
            var comment = new CommentService().BringById(Convert.ToInt32(Session["SelectedCommentId"]));
            model.CommentId = comment.CommentId;
            model.PostId = comment.PostId;
            model.UserId = comment.UserId;
            model.InsertedDate = comment.InsertedDate;

            var result = new CommentService().Update(model);

            if (result)
            {
                TempData["ServiceResult"] = "Comment update process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while updating the comment.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
    }
}