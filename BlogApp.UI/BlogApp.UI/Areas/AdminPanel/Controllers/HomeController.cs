using BlogApp.BLL.Services;
using BlogApp.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogApp.UI.Areas.AdminPanel.Controllers
{
    [Filters.AutorizeUser]
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}