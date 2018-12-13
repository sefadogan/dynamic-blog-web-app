
using BlogApp.BLL.Services;
using BlogApp.UI.Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int page = 1)
        {
            var postList = new PostService().ListAll().Where(x => x.IsActive == true).OrderByDescending(x=> x.InsertedDate).ToPagedList(page, 5);
            return View(postList);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult SendEmail(string name, string surname, string email, string message)
        {
            JsonResult jsonResult = new JsonResult();

            bool isSentSuccess = MailSender.ContactSendMail(email,
                    "Sefa DOĞAN Kişisel Web Sitesi İletişim Talebi",
                    string.Format(
                        "<p><b>Ad: </b> {0}</p>" +
                        "<p><b>Soyad: </b> {0}</p>" +
                        "<p><b>Email: </b> {2}</p>" +
                        "<p><b>Mesaj: </b> {3}</p>", name, surname, email, message), new List<string>() { "sefa@sefadogan.com" });

            if (isSentSuccess)
                jsonResult.Data = new { success = true, message = "iletişim talebiniz başarıyla kaydedilmiştir." };
            else
                jsonResult.Data = new { success = false, message = "iletişim talebiniz kaydedilirken bir hata oluştu lütfen daha sonra tekrar deneyiniz." };

            return jsonResult;
        }
    }
}