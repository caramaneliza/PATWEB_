using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.Web.App_Start;
using BookMall.Web.Models; 

namespace BookMall.Web.Controllers
{
    public class SettingsController : BaseController
    {
        // GET: Settings

        User user = new User { 
            Email = "catalinsfake@gmail.com",
            FirstName = "Catain",
            LastName = "Fake",
            PasswordHash = "sakdjf;sadfk2134j",
            Privilege = "God" };

        [UserMod]
        public ActionResult Index()
        {
            SessionStatus();
            GetUsername();
            return View(user);
        }
    }
}