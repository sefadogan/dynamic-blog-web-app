using BlogApp.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            if (Session["UserIsLogedIn"] != null)
            {
                return RedirectToAction("List", "Post");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = new UserService().Login(email, password);

            if (user != null && user.Role.Name.Trim() == "Admin")
            {
                Session["UserIsLogedIn"] = true;
                Session["LoggedInUser"] = user;
                return RedirectToAction("List", "Post");
            }

            else if (user != null && user.Role.Name.Trim() == "Member")
            {
                ViewBag.ServiceResult = "You do not have permission to log in with this account.";
                ViewBag.Type = "danger";
                return View();
            }

            ViewBag.ServiceResult = "Your email adress or your password is incorrect.";
            ViewBag.Type = "danger";
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserIsLogedIn"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}