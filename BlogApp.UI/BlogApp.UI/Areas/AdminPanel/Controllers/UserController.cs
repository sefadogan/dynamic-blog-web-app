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
    public class UserController : Controller
    {
        public ActionResult List(int page = 1)
        {
            var userList = new UserService().ListAll().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(userList);
        }
        public ActionResult Edit(int id)
        {

            Session["SelectedUserId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var user = new UserService().BringById(id);

            if (user != null)
            {
                ViewData["RoleList"] = new RoleService().ListAll();
                return View(user);
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Create()
        {
            ViewData["RoleList"] = new RoleService().ListAll();
            return View();
        }
        public ActionResult Detail(int id)
        {
            var user = new UserService().BringById(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Delete(int id)
        {
            var result = new UserService().Delete(id);

            if (result)
            {
                TempData["ServiceResult"] = "User delete process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while deleting the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            model.InsertedDate = DateTime.Now;

            var result = new UserService().Add(model);
            if (result)
            {
                TempData["ServiceResult"] = "User create process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                //TempData["ServiceResult"] = "There was an error while creating the user.";
                //TempData["AlertType"] = "danger";
                //return RedirectToAction("List");
                ViewData["RoleList"] = new RoleService().ListAll();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            var user = new UserService().BringById(Convert.ToInt32(Session["SelectedUserId"]));

            model.UserId = user.UserId;
            model.InsertedDate = user.InsertedDate;

            var result = new UserService().Update(model);

            if (result)
            {
                TempData["ServiceResult"] = "User update process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while updating the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
    }
}