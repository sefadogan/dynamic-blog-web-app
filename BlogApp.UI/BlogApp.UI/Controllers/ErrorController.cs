using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult InternalServer()
        {
            return View();
        }
        public ActionResult NoAuthorization()
        {
            return View();
        }
    }
}