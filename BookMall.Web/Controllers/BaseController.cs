using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.BuisnessLogic;
using BookMall.BuisnessLogic.Interfaces;
using BookMall.Domain.Entities.User;
using BookMall.Domain.Enums;
using BookMall.Web.Models;

namespace BookMall.Web.Controllers
{
    public class BaseController : Controller
    {
        public readonly ISession _session;
        // GET: Base
        public BaseController()
        {
            var bl = new BuissnesLogic();
            _session = bl.GetSessionBL();
        }
        public void SessionStatus()
        {
            var apiCookie = Request.Cookies["bm_token"];
            if (apiCookie != null)
            {
                var uProfile = _session.GetUserByCookie(apiCookie.Value);
                if (uProfile != null)
                {
                    System.Web.HttpContext.Current.Session.Add("__SessionObject", uProfile);
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                }
                else
                {
                    System.Web.HttpContext.Current.Session.Clear();
                    if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("bm_token"))
                    {
                        var cookie = ControllerContext.HttpContext.Request.Cookies["bm_token"];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            }
        }

        public void GetUsername()
        {
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                var user = (UProfileData)System.Web.HttpContext.Current?.Session["__SessionObject"];
                ViewBag.Username = user.Username;
            }
            else
            {
                ViewBag.Username = "Guest";
            }
        }

        public void GetUserLevel()
        {
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] == "login")
            {
                var user = (UProfileData)System.Web.HttpContext.Current?.Session["__SessionObject"];
                switch (user.Level)
                {
                    case URole.Admin:
                        ViewBag.UserLevel = "Admin";
                        break;
                    case URole.Moderator:
                        ViewBag.UserLevel = "Moderator";
                        break;
                    case URole.User:
                        ViewBag.UserLevel = "User";
                        break;
                    default:
                        break;
                }
                ViewBag.FirstLogin = user.FirstLogin.ToString("dd MMMM yyyy");
            }
        }

        public UserSettings GetUserMinimal()
        {
            GetUserLevel();
            var apiCookie = Request.Cookies["bm_token"];
            if (apiCookie != null)
            {
                var uProfile = _session.GetUserByCookie(apiCookie.Value);
                return new UserSettings
                {
                    Id = uProfile.Id,
                    Email = uProfile.Email,
                    Username = uProfile.Username,
                    Privilege = ViewBag.UserLevel,
                    CurrentPassword = "",
                    Password1 = "",
                    Password2 = "",
                };
            }
            return new UserSettings();

        }
    }
}
