using BlogApp.BLL.Services;
using BlogApp.DAL.Entity;
using BlogApp.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Detail(int id)
        {
            List<VMHierarchicalCommentList> hierarchicalCommentList = new List<VMHierarchicalCommentList>();

            List<Comment> allComments = new CommentService().ListAll().Where(c => c.Status == 1 && c.PostId == id).ToList(); // 0: Passive, 1: Active, 2: Pending.
            foreach (var item in allComments.Where(c => c.ParentCommentId == 0).ToList())
            {
                VMHierarchicalCommentList parentComment = new VMHierarchicalCommentList()
                {
                    CommentId = item.ParentCommentId,
                    Text = item.Text,
                    InsertedDate = item.InsertedDate,
                    UserId = item.UserId,
                    UserFirstName = item.User.FirstName,
                    UserLastName = item.User.LastName
                };

                FillChildComment(parentComment, item.CommentId, allComments);
                hierarchicalCommentList.Add(parentComment);
            }

            ViewData["CommentList"] = hierarchicalCommentList; // All comments about this post.

            var topThreePosts = new PostService().ListAll().OrderByDescending(x => x.InsertedDate).Skip(0).Take(3).ToList();
            ViewData["TopThreePosts"] = topThreePosts; // This is for _PartialAlsoLike.

            var post = new PostService().BringById(id);
            return View(post);
        }

        public ActionResult SendComment(User user, int postId, string text, int parentId = 0)
        {
            try
            {
                var post = new PostService().BringById(postId);

                user.InsertedDate = DateTime.Now;
                user.IsActive = true;
                user.RoleId = 4;

                var userResult = new UserService().Add(user);
                if (!userResult)
                    return View();

                Comment comment = new Comment
                {
                    PostId = postId,
                    InsertedDate = DateTime.Now,
                    ParentCommentId = parentId,
                    Status = 2,
                    Text = text,
                    UserId = user.UserId
                };

                var commentResult = new CommentService().Add(comment);

                return RedirectToAction("Detail");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void FillChildComment(VMHierarchicalCommentList hierarchicalCommentList, int parentCommentId, List<Comment> allComments)
        {
            foreach (var item in allComments.Where(c => c.ParentCommentId == parentCommentId).ToList())
            {
                VMHierarchicalCommentList childComment = new VMHierarchicalCommentList()
                {
                    CommentId = item.CommentId,
                    Text = item.Text,
                    InsertedDate = item.InsertedDate,
                    UserId = item.UserId,
                    UserFirstName = item.User.FirstName,
                    UserLastName = item.User.LastName
                };

                hierarchicalCommentList.ChildComments.Add(childComment);
                

                FillChildComment(childComment, childComment.CommentId, allComments);
            }
        }
    }
}