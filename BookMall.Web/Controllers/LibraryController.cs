using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.Web.Filters;

namespace BookMall.Web.Controllers
{
    public class LibraryController : BaseController
    {
        // GET: Library
        [UserMod]
        public ActionResult Index()
        {
                SessionStatus();
                GetUsername();
                return View();
        }
    }
}