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
           
            //UserData u = new UserData();
            //u.Username = "catalin";

            //return View(u);

            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                var user = (UProfileData)System.Web.HttpContext.Current?.Session["__SessionObject"];
                UserProfile up = new UserProfile
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    ProfileUrl = user.Level == URole.Admin ? "/Admin" : "/profile"
                };
                return View(up);
            }
            else
            {
                return View();
            }
        }
    }
}