using BlogApp.BLL.Abstract;
using BlogApp.DAL.Entity;
using BlogApp.UI.Areas.AdminPanel.Filters;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [CustomAuthorization]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(
            IUserService userService,
            IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public ActionResult List(int page = 1)
        {
            var userList = _userService.GetList().OrderByDescending(x => x.InsertedDate).ToPagedList(page, 20);
            return View(userList);
        }
        public ActionResult Edit(int id)
        {
            Session["SelectedUserId"] = id; // Bilgiler post edildiğinde post metodunda yakalayabilmek adına oluşturuldu.

            var user = _userService.Get(u => u.UserId == id);
            if (user == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            ViewData["RoleList"] = _roleService.GetList();
            return View(user);
        }
        public ActionResult Create()
        {
            ViewData["RoleList"] = _roleService.GetList();
            return View();
        }
        public ActionResult Detail(int id)
        {
            var user = _userService.Get(u => u.UserId == id);
            if (user == null)
            {
                TempData["ServiceResult"] = "There was an error while viewing the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            return View(user);
        }
        public ActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while deleting the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "User delete process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            model.InsertedDate = DateTime.Now;

            var result = _userService.Add(model);
            if (!result)
            {
                //TempData["ServiceResult"] = "There was an error while creating the user.";
                //TempData["AlertType"] = "danger";
                //return RedirectToAction("List");
                ViewData["RoleList"] = _roleService.GetList();
                return View(model);
            }

            TempData["ServiceResult"] = "User create process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            int convertedUserId = Convert.ToInt32(Session["SelectedUserId"]); // "Get Method" is taking a paramater as expression. So i converted session id then compared them.

            var user = _userService.Get(u => u.UserId == convertedUserId);
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsActive = model.IsActive;
            user.Password = model.Password;
            user.RoleId = model.RoleId;
            user.Username = model.Username;

            var result = _userService.Update(user);
            if (!result)
            {
                TempData["ServiceResult"] = "There was an error while updating the user.";
                TempData["AlertType"] = "danger";
                return RedirectToAction("List");
            }

            TempData["ServiceResult"] = "User update process was successful.";
            TempData["AlertType"] = "success";
            return RedirectToAction("List");
        }
    }
}