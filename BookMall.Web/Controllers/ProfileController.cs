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

        UserMinimal user = new UserMinimal
        {
            Email = "catalinsfake@gmail.com",
            Username = "Catain",
            Privilege = "God"
        };

        [UserMod]
        public ActionResult Index()
        {
            SessionStatus();
            GetUsername();
            GetUserLevel();
            return View(user);
        }
    }
}