using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.BuisnessLogic;
using BookMall.BuisnessLogic.Interfaces;

namespace BookMall.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ISession _session;
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
    }
}