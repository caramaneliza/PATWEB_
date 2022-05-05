using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMall.Web.Controllers
{
    public class LibraryController : BaseController
    {
        // GET: Library
        public ActionResult Index()
        {
            return View();
        }
    }
}