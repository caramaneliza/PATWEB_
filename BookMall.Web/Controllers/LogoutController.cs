using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMall.Web.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
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

            return RedirectToAction("Index", "Home");
        }
    }
}