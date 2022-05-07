using System.Web;
using System.Web.Mvc;
using BookMall.BuisnessLogic.Interfaces;
using BookMall.BuisnessLogic;
using System.Web.Routing;
using BookMall.Domain.Enums;
using BookMall.Domain.Entities.User;

namespace BookMall.Web.Filters
{
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

            if (adminSession != null)
            {

                var cookie = HttpContext.Current.Request.Cookies["bm_token"];
                if (cookie != null)
                {
                    var profile = _session.GetUserByCookie(cookie.Value);
                    if (profile != null && profile.Level == URole.Admin)
                    {
                        HttpContext.Current.Session.Add("__SessionObject", profile);
                        return;
                    }
            }
            filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary(new { controller = "Error", action = "Error404" }));
        }
    }
}