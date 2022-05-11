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
            var pdbBok = _product.GetSingleProduct(id);
            var book = new Book()
            {
                Author = pdbBok.Author,
                Title = pdbBok.Title,
                Description = pdbBok.Description,
                Genre = pdbBok.Genre,
                ImageUrl = pdbBok.ImageUrl,
                PdfUrl = pdbBok.PdfUrl,
                JpgFile = pdbBok.JpgFile,
                PdfFile = pdbBok.PdfFile,
                Price = pdbBok.Price,
                Pages = pdbBok.Pages,
                ISBN = pdbBok.ISBN
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

        [ModeratorMod]
        public ActionResult Edit(int id)
        {
            SessionStatus();
            GetUsername();
            GetUserLevel();
            ViewBag.id = id;
            var pdbBok = _product.GetSingleProduct(id);
            var book = new Book()
            {
                Id = pdbBok.Id,
                Author = pdbBok.Author,
                Title = pdbBok.Title,
                Description = pdbBok.Description,
                Genre = pdbBok.Genre,
                ImageUrl = pdbBok.ImageUrl,
                PdfUrl = pdbBok.PdfUrl,
                JpgFile = pdbBok.JpgFile,
                PdfFile = pdbBok.PdfFile,
                Price = pdbBok.Price,
                Pages = pdbBok.Pages,
                ISBN = pdbBok.ISBN
            };

            return View(book);
        }


        [ModeratorMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, Book book)
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
                Id = book.Id,
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

            var bookCreate = _product.EditProduct(data);
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




        [ModeratorMod]
        public string Delete(int id)
        {
            var response = _product.DeleteProductById(id);
            return response.StatusMsg;
        }
    }
}