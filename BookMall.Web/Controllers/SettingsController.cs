﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.Web.Models;
using BookMall.Web.Filters;

namespace BookMall.Web.Controllers
{
    public class SettingsController : BaseController
    {
        // GET: Settings

        UserMinimal user = new UserMinimal { 
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
            GetUserLevel();
            return View(user);
        }
    }
}