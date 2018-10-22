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
    public class RoleController : Controller
    {
        public ActionResult List(int page = 1)
        {
            var roleList = new RoleService().ListAll().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(roleList);
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedRoleId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var role = new RoleService().BringById(id);

            if (role != null)
            {
                return View(role);
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            var role = new RoleService().BringById(id);

            if (role != null)
            {
                return View(role);
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while viewing the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
        public ActionResult Delete(int id)
        {
            var result = new RoleService().Delete(id);

            if (result)
            {
                TempData["ServiceResult"] = "Role delete process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while deleting the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public ActionResult Create(Role model)
        {
            model.InsertedDate = DateTime.Now;

            var result = new RoleService().Add(model);
            if (result)
            {
                TempData["ServiceResult"] = "Role create process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while creating the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public ActionResult Edit(Role model)
        {
            var role = new RoleService().BringById(Convert.ToInt32(Session["SelectedRoleId"]));
            model.RoleId = role.RoleId;
            model.InsertedDate = role.InsertedDate;

            var result = new RoleService().Update(model);

            if (result)
            {
                TempData["ServiceResult"] = "Role update process was successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ServiceResult"] = "There was an error while updating the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }
        }
    }
}