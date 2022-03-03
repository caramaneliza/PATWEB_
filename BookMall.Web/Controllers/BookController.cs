using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMall.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        [Route("book/{id?}")]
        public ActionResult Index(string id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}