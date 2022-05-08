using System;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using BookMall.Web.Filters;
using BookMall.Web.Models;

namespace BookMall.Web.Controllers
{
    public class BookController : BaseController
    {

        // GET: Book
        //[Route("book/{id?}")]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Display(int id)
        {
            SessionStatus();
            GetUsername();
            GetUserLevel();
            ViewBag.id = id;

            Random r = new Random();
            var book = new Book()
            {
                Author = "David Thomas",
                Title = "The Pragmatic Programmer: 20th Anniversary Edition, 2nd Edition: Your Journey to Mastery",
                Description = "Microsoft and our third-party vendors use cookies to store and access information such as unique IDs to deliver, maintain and improve our services and ads. If you agree, MSN and Microsoft Bing will personalise the content and ads that you see. You can select ‘I Accept’ to consent to these uses or click on ‘Manage preferences’ to review your options and exercise your right to object to Legitimate Interest where used.  You can change your selection under ‘Manage Preferences’ at the bottom of this page. Privacy Statement.",
                Genre = "Programming",
                ImageUrl = "https://m.media-amazon.com/images/I/51A8l+FxFNL.jpg",
                PdfFile = "https://m.media-amazon.com/images/I/51A8l+FxFNL.jpg",
                Price = (float)r.NextDouble() * 100,
                Pages = r.Next(50, 2000),
                ISBN = "1-4028-9462-7"
            };

            return View(book);
        }

        [ModeratorMod]
        public ActionResult Create()
        {
            SessionStatus();
            GetUsername();
            GetUserLevel();

            return View();
        }
        [ModeratorMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, Book book)
        {
            string fileName;
            FileInfo fi = new FileInfo(file.FileName);
            if (file != null && file.ContentLength > 0)
            {
                using (var md5 = MD5.Create()) {
                    fileName = BitConverter.ToString(md5.ComputeHash(file.InputStream)).Replace("-", "");
                    var path = Path.Combine(Server.MapPath("~/UserFiles/Books"), fileName + fi.Extension);
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(int id)
        {
            return View();
        }

        public string Delete(string id)
        {
            return "Book " + id.ToString() + " was deleted successfully";
        }
    }
}