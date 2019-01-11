using BlogApp.BLL.Services;
using BlogApp.DAL.Entity;
using BlogApp.Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [Filters.AutorizeUser]
    public class CategoryController : Controller
    {
        public ActionResult List(int page = 1)
        {
            var categoryList = new CategoryService().ListAll().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(categoryList);
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedCategoryId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var category = new CategoryService().BringById(id);

            if (category != null)
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

                return View(category);
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the category.";
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
            var category = new CategoryService().BringById(id);

            if (category != null)
            {
                return View(category);
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Delete(int id)
        {
            var result = new CategoryService().Delete(id);

            if (result)
            {
                TempData["ServiceResult"] = "Category delete process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while deleting the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            model.InsertedDate = DateTime.Now;
            if(model.ParentCategoryId != 0)
                model.RootLevel = new CategoryService().BringById(model.ParentCategoryId).RootLevel + 1;
            
            var result = new CategoryService().Add(model);
            
            if (result)
            {
                TempData["ServiceResult"] = "Category create process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while creating the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public ActionResult Edit(Category model)
        {
            var category = new CategoryService().BringById(Convert.ToInt32(Session["SelectedCategoryId"]));
            model.CategoryId = category.CategoryId;
            model.ParentCategoryId = category.ParentCategoryId;
            model.InsertedDate = category.InsertedDate;

            var result = new CategoryService().Update(model);

            if (result)
            {
                TempData["ServiceResult"] = "Category update process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while updating the category.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        public void FillChildCategory(VMHierarchicalCategoryList hierarchicalCategoryList, int parentCategoryId, List<Category> allCategories, List<SelectListItem> ddlHierarchicalCategory)
        {
            //int counter = 1;
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