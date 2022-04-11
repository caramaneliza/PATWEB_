using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.BuisnessLogic;
using BookMall.BuisnessLogic.Interfaces;
using BookMall.Domain.Entities.User;
using BookMall.Web.Models;

namespace BookMall.Web.Controllers
{
    public class SignupController : Controller
    {
        private readonly ISession _session;
        public SignupController()
        {
            var bl = new BuissnesLogic();
            _session = bl.GetSessionBL();
        }

        // GET: Signup
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserSignup signup)
        {
            if (ModelState.IsValid)
            {
                USignupData data = new USignupData
                {
                    Username = signup.Username,
                    Email = signup.Email,
                    Password1 = signup.Password1,
                    Password2 = signup.Password2,
                    LoginIp = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                var userSignup = _session.UserSignup(data);
                if (userSignup.Status)
                {
                    ULoginData data0 = new ULoginData
                    {
                        Credential = signup.Email,
                        Password = signup.Password1, 
                        LoginIp = Request.UserHostAddress,
                        LoginDateTime = DateTime.Now
                    };
                    var cookie = _session.GenUserCookie(data0);
                    //Add COOKIE
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userSignup.StatusMsg);
                    return View();
                }
            }

            return View();
        }
    }
}