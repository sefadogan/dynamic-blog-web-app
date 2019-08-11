using BlogApp.BLL.Abstract;
using BlogApp.DAL.Entity;
using BlogApp.Model.ViewModel;
using BlogApp.UI.Areas.AdminPanel.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [CustomAuthorization]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public ActionResult List(int page = 1)
        {
            var categoryList = _categoryService.GetList().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(categoryList);
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedCategoryId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var category = _categoryService.Get(c => c.CategoryId == id);

            if (category == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

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
            return View(category);
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
            var category = _categoryService.Get(c => c.CategoryId == id);
            if (category == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            return View(category);
        }
        public ActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while deleting the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Category delete process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (model.ParentCategoryId != 0)
                model.RootLevel = _categoryService.Get(c => c.CategoryId == model.ParentCategoryId).RootLevel + 1;

            model.InsertedDate = DateTime.Now;
            var result = _categoryService.Add(model);

            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while creating the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Category create process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            int convertedCategoryId = Convert.ToInt32(Session["SelectedCategoryId"]); // "Get Method" is taking a paramater as expression. So i converted session id then compared them.
            var category = _categoryService.Get(c => c.CategoryId == convertedCategoryId);
            category.Description = model.Description;
            category.IsActive = model.IsActive;
            category.Name = model.Name;
            category.ParentCategoryId = model.ParentCategoryId;

            var result = _categoryService.Update(category);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while updating the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Category update process was successful.";
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