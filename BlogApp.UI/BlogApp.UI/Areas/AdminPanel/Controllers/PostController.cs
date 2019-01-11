
using BlogApp.BLL.Services;
using BlogApp.DAL.Entity;
using BlogApp.Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [Filters.AutorizeUser]
    public class PostController : Controller
    {
        User loggedInUser = new User();

        public ActionResult List(int page = 1)
        {
            var postList = new PostService().ListAll().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(postList);
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedPostId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var post = new PostService().BringById(id);

            //ViewData["CategoryList"] = new CategoryService().ListAll();

            if (post != null)
            {
                List<SelectListItem> ddlHierarchicalCategory = new List<SelectListItem>();
                List<VMHierarchicalCategoryList> hierarchicalCategoryList = new List<VMHierarchicalCategoryList>();

                List<Category> allCategories = new CategoryService().ListAll().Where(c => c.IsActive).ToList();
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
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Create()
        {
            List<SelectListItem> ddlHierarchicalCategory = new List<SelectListItem>();
            List<VMHierarchicalCategoryList> hierarchicalCategoryList = new List<VMHierarchicalCategoryList>();

            List<Category> allCategories = new CategoryService().ListAll().Where(c => c.IsActive).ToList();
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
            var post = new PostService().BringById(id);

            if (post != null)
            {
                return View(post);
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Delete(int id)
        {
            var result = new PostService().Delete(id);

            if (result)
            {
                TempData["ServiceResult"] = "Post delete process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while updating the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
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

            var result = new PostService().Add(model);
            if (result)
            {
                TempData["ServiceResult"] = "Post create process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while creating the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post model)
        {
            loggedInUser = (User)Session["LoggedInUser"];

            var post = new PostService().BringById(Convert.ToInt32(Session["SelectedPostId"]));
            model.UserId = loggedInUser.UserId;
            model.PostId = post.PostId;
            model.TimesDisplayed = post.TimesDisplayed;
            model.InsertedDate = post.InsertedDate;

            var result = new PostService().Update(model);

            if (result)
            {
                TempData["ServiceResult"] = "Post update process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while updating the post.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
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