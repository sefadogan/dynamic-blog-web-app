using BlogApp.UI.Areas.AdminPanel.Filters;
using System.Web.Mvc;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [CustomAuthorization]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}