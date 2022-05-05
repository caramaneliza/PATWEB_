using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using BookMall.Domain.Entities.User;
using BookMall.BuisnessLogic.DBModel;
using BookMall.Web.Models;
using BookMall.Domain.Enums;

namespace BookMall.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
           
            SessionStatus();
            UserProfile user_profile;
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                var user = (UProfileData)System.Web.HttpContext.Current?.Session["__SessionObject"];
                user_profile = new UserProfile
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    ProfileUrl = user.Level == URole.Admin ? "/Admin" : "/profile"
                };
                ViewBag.Username = user.Username;
            }
            else
            {
                user_profile = new UserProfile { };
                ViewBag.Username = "Guest";
            }
            return View(user_profile);
        }
    }
}