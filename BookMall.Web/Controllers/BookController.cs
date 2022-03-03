using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.Web.Models;

namespace BookMall.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        [Route("book/{id?}")]
        public ActionResult Index(string id)
        {
            ViewBag.id = id;

            Random r = new Random();
            var book = new Book() { 
                Author = "David Thomas", 
                Title = "The Pragmatic Programmer: 20th Anniversary Edition, 2nd Edition: Your Journey to Mastery", 
                Genre = "Programming", 
                Img = "https://m.media-amazon.com/images/I/51A8l+FxFNL.jpg",
                Price = (float)r.NextDouble() * 100, 
                Pages = r.Next(50, 2000) };

            return View(book);
        }
    }
}