using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.Web.Models; 

namespace BookMall.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile

        User user = new User { 
            Email = "catalinsfake@gmail.com",
            FirstName = "Catain",
            LastName = "Fake",
            PasswordHash = "sakdjf;sadfk2134j",
            Privilege = "God" };

        public ActionResult Index()
        {
            return View(user);
        }
    }
}