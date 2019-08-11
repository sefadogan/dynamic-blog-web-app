
using BlogApp.BLL.Abstract;
using BlogApp.DAL.Entity;
using BlogApp.Model.ViewModel;
using BlogApp.UI.Areas.AdminPanel.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [CustomAuthorization]
    public class PostController : Controller
    {
        private User loggedInUser = new User();
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public PostController(
            IPostService postService,
            ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        public ActionResult List(int page = 1)
        {
            var postList = _postService.GetList().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(postList);
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedPostId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var post = _postService.Get(p => p.PostId == id);

            if (post == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            List<SelectListItem> ddlHierarchicalCategory = new List<SelectListItem>();
            List<VMHierarchicalCategoryList> hierarchicalCategoryList = new List<VMHierarchicalCategoryList>();

            List<Category> allCategories = _categoryService.GetList().Where(c => c.IsActive).ToList();
            foreach (var item in allCategories.Where(c => c.ParentCategoryId == 0).ToList())
            {
                VMHierarchicalCategoryList parentCategory = new VMHierarchicalCategoryList()
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Description = item.Description
                };

                ddlHierarchicalCategory.Add(new SelectListItem()
                {
                    Text = "• " + item.Name,
                    Value = item.CategoryId.ToString()
                });

                FillChildCategory(parentCategory, item.CategoryId, allCategories, ddlHierarchicalCategory);
                hierarchicalCategoryList.Add(parentCategory);
            }

            ViewData["HierarchicalCategoryList"] = ddlHierarchicalCategory;
            return View(post);
        }
        public ActionResult Create()
        {
            List<SelectListItem> ddlHierarchicalCategory = new List<SelectListItem>();
            List<VMHierarchicalCategoryList> hierarchicalCategoryList = new List<VMHierarchicalCategoryList>();

            var allCategories = _categoryService.GetList().Where(c => c.IsActive).ToList();
            foreach (var item in allCategories.Where(c => c.ParentCategoryId == 0).ToList())
            {
                VMHierarchicalCategoryList parentCategory = new VMHierarchicalCategoryList()
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Description = item.Description
                };

                ddlHierarchicalCategory.Add(new SelectListItem()
                {
                    Text = "• " + item.Name,
                    Value = item.CategoryId.ToString()
                });

                FillChildCategory(parentCategory, item.CategoryId, allCategories, ddlHierarchicalCategory);
                hierarchicalCategoryList.Add(parentCategory);
            }

            ViewData["HierarchicalCategoryList"] = ddlHierarchicalCategory;
            return View();
        }
        public ActionResult Detail(int id)
        {
            var post = _postService.Get(p => p.PostId == id);
            if (post == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            return View(post);
        }
        public ActionResult Delete(int id)
        {
            var result = _postService.Delete(id);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while updating the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Post delete process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Post model)
        {
            loggedInUser = (User)Session["LoggedInUser"];

            model.UserId = loggedInUser.UserId;
            model.InsertedDate = DateTime.Now;
            model.TimesDisplayed = 0;

            var result = _postService.Add(model);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while creating the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Post create process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post model)
        {
            loggedInUser = (User)Session["LoggedInUser"];

            var convertedPostId = Convert.ToInt32(Session["SelectedPostId"]); // "Get Method" is taking a paramater as expression. So i converted session id then compared them.
            var post = _postService.Get(p => p.PostId == convertedPostId);

            // TODO : refactor
            post.Body = model.Body;
            post.BodySummary = model.BodySummary;
            post.CategoryId = model.CategoryId;
            post.IsActive = model.IsActive;
            post.MetaKeywords = model.MetaKeywords;
            post.SeoUrl = model.SeoUrl;
            post.Title = model.Title;
            post.UserId = loggedInUser.UserId;

            var result = _postService.Update(post);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while updating the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Post update process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        public void FillChildCategory(VMHierarchicalCategoryList hierarchicalCategoryList, int parentCategoryId, List<Category> allCategories, List<SelectListItem> ddlHierarchicalCategory)
        {
            foreach (var item in allCategories.Where(c => c.ParentCategoryId == parentCategoryId).ToList())
            {
                VMHierarchicalCategoryList childCategory = new VMHierarchicalCategoryList()
                {
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Description = item.Description
                };

                hierarchicalCategoryList.ChildCategories.Add(childCategory);

                ddlHierarchicalCategory.Add(new SelectListItem()
                {
                    Text = "".PadLeft(item.RootLevel, '-') + "> " + item.Name,
                    Value = item.CategoryId.ToString()
                });

                FillChildCategory(childCategory, childCategory.CategoryId, allCategories, ddlHierarchicalCategory);
            }
        }
    }
}