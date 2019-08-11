using BlogApp.BLL.Abstract;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login()
        {
            if (Session["UserIsLogedIn"] == null)
            {
                return View();
            }

            return RedirectToAction("List", "Post");
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = _userService.Login(email, password);
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