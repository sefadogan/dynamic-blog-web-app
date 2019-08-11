
using BlogApp.BLL.Abstract;
using BlogApp.UI.Helpers;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BlogApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public ActionResult Index(int page = 1)
        {
            var posts = _postService.GetList(x=>x.IsActive).OrderByDescending(x => x.InsertedDate).ToPagedList(page, 5);
            return View(posts);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public JsonResult SendEmail(string name, string surname, string email, string message)
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