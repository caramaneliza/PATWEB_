using System;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using BookMall.BuisnessLogic;
using BookMall.BuisnessLogic.Interfaces;
using BookMall.Domain.Entities.Product;
using BookMall.Domain.Entities.User;
using BookMall.Web.Filters;
using BookMall.Web.Models;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

namespace BookMall.Web.Controllers
{
    public class BookController : BaseController
    {
        private readonly IProduct _product;
        public BookController()
        {
            var bl = new BuissnesLogic();
            _product = bl.GetProductBL();
        }

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
                PdfUrl = "https://m.media-amazon.com/images/I/51A8l+FxFNL.jpg",
                JpgFile = "https://m.media-amazon.com/images/I/51A8l+FxFNL.jpg",
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
            if (file != null)
            {
                FileInfo fi = new FileInfo(file.FileName);
                if (file != null && file.ContentLength > 0)
                {
                    using (var md5 = MD5.Create())
                    {
                        fileName = BitConverter.ToString(md5.ComputeHash(file.InputStream)).Replace("-", "");
                        book.ImageUrl = "~/Content/Covers/" + fileName + ".jpg";
                        book.PdfUrl = "~/Content/Books/" + fileName + fi.Extension;
                        book.PdfFile = Path.Combine(Server.MapPath("~/Content/Books/"), fileName + fi.Extension);
                        book.JpgFile = Path.Combine(Server.MapPath("~/Content/Covers/"), fileName + ".jpg");
                        if (!System.IO.File.Exists(book.PdfFile))
                        {
                            file.SaveAs(book.PdfFile);
                        }
                    }
                }


                Document pdfDocument = new Document(book.PdfFile);
                book.Pages = pdfDocument.Pages.Count;

                Resolution resolution = new Resolution(300);
                JpegDevice JpegDevice = new JpegDevice(500, 700, resolution);
                if (!System.IO.File.Exists(book.JpgFile))
                {
                    JpegDevice.Process(pdfDocument.Pages[1], book.JpgFile);
                }
            }

            SessionStatus();
            var user = (UProfileData)System.Web.HttpContext.Current?.Session["__SessionObject"];

            PDbTable data = new PDbTable
            {
                OwnerId = user.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                ImageUrl = book.ImageUrl,
                PdfUrl = book.PdfUrl,
                JpgFile = book.JpgFile,
                PdfFile = book.PdfFile,
                Price = book.Price,
                Pages = book.Pages,
                ISBN = book.ISBN
            };

            var bookCreate = _product.CreateProduct(data);
            if (bookCreate.Status)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", bookCreate.StatusMsg);
                return View();
            }
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