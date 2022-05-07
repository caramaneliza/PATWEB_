using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.Web.Models;
using BookMall.Web.App_Start;
using BookMall.Web.Filters;

namespace BookMall.Web.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Profile
        [UserMod]
        public ActionResult Index()
        {
            SessionStatus();
            GetUsername();
            GetUserLevel();
            return View(GetUserMinimal());
        }
    }
}