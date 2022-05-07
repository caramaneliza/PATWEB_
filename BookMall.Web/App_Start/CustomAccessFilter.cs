using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.BuisnessLogic.Interfaces;
using BookMall.BuisnessLogic;
using System.Web.Routing;
using BookMall.Domain.Enums;
using BookMall.Domain.Entities.User;

namespace BookMall.Web.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessCustomAccesFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Url != null &&
                (filterContext.HttpContext.Request.UrlReferrer == null ||
                filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Error", action = "Error500" }));
            }
        }
    }
    public class AdminModAttribute : ActionFilterAttribute
    {
        private readonly ISession _session;
        public AdminModAttribute()
        {
            var bl = new BuissnesLogic();
            _session = bl.GetSessionBL();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var adminSession = (UProfileData)HttpContext.Current?.Session["__SessionObject"];

            if (adminSession == null)
            {
                var cookie = HttpContext.Current.Request.Cookies["bm_token"];
                if (cookie != null)
                {
                    var profile = _session.GetUserByCookie(cookie.Value);
                    if (profile != null && profile.Level == URole.Admin)
                    {
                        HttpContext.Current.Session.Add("__SessionObject", profile);
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(new { controller = "Error", action = "Error404" }));
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Error", action = "Error404" }));
                }
            }
        }
    }
    public class ModeratorModAttribute : ActionFilterAttribute
    {
        private readonly ISession _session;
        public ModeratorModAttribute()
        {
            var bl = new BuissnesLogic();
            _session = bl.GetSessionBL();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var adminSession = (UProfileData)HttpContext.Current?.Session["__SessionObject"];
            if (adminSession == null)
            {
                var cookie = HttpContext.Current.Request.Cookies["bm_token"];
                if (cookie != null)
                {
                    var profile = _session.GetUserByCookie(cookie.Value);
                    if (profile != null && profile.Level == URole.Moderator)
                    {
                        HttpContext.Current.Session.Add("__SessionObject", profile);
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(new { controller = "Error", action = "Error404" }));
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Error", action = "Error404" }));
                }
            }
        }
    }
   
}