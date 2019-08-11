using BlogApp.BLL.Abstract;
using BlogApp.DAL.Entity;
using BlogApp.UI.Areas.AdminPanel.Filters;
using PagedList;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [CustomAuthorization]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public ActionResult List(int page = 1)
        {
            var roleList = _roleService.GetList().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(roleList);
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedRoleId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.
            var role = _roleService.Get(r => r.RoleId == id);

            if (role == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            return View(role);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Detail(int id)
        {
            var role = _roleService.Get(r => r.RoleId == id);
            if (role == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            return View(role);
        }
        public ActionResult Delete(int id)
        {
            var result = _roleService.Delete(id);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while deleting the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");

            }

            TempData["ServiceResult"] = "Role delete process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Create(Role model)
        {
            model.InsertedDate = DateTime.Now;

            var result = _roleService.Add(model);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while creating the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Role create process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(Role model)
        {
            int convertedRoleId = Convert.ToInt32(Session["SelectedRoleId"]); // "Get Method" is taking a paramater as expression. So i converted session id then compared them.

            var role = _roleService.Get(r => r.RoleId == convertedRoleId);
            role.Name = model.Name;

            var result = _roleService.Update(role);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while updating the role.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "Role update process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }
    }
}