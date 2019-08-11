using BlogApp.BLL.Abstract;
using BlogApp.DAL.Entity;
using BlogApp.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BlogApp.UI.Controllers
{
    public class PostController : Controller
    {
        // TODO : Gonna implement "Unit of work" pattern.
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public PostController(
            IPostService postService,
            ICommentService commentService,
            IUserService userService)
        {
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
        }

        [Route("{seoUrl}-{postId:int}")]
        public ActionResult Detail(int postId)
        {
            List<VMHierarchicalCommentList> hierarchicalCommentList = new List<VMHierarchicalCommentList>();

            var allComments = _commentService.GetList().Where(c => c.Status == 1 && c.PostId == postId).ToList(); // 0: Passive, 1: Active, 2: Pending.
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

            var topThreePosts = _postService.GetList().Where(x => x.IsActive == true).OrderByDescending(x => x.InsertedDate).Skip(0).Take(3).ToList();
            ViewData["TopThreePosts"] = topThreePosts; // This is for _PartialAlsoLike.

            var post = _postService.Get(p => p.PostId == postId);
            return View(post);
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