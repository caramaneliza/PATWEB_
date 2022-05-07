using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.Web.Models;
using BookMall.Web.Filters;
using BookMall.Domain.Entities.User;

namespace BookMall.Web.Controllers
{
    public class SettingsController : BaseController
    {
        // GET: Settings
        [UserMod]
        public ActionResult Index()
        {
            SessionStatus();
            GetUsername();
            GetUserLevel();

            return View(GetUserMinimal());
        }

        [UserMod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserSettings settings)
        {
            if (ModelState.IsValid)
            {
                USettingsData data = new USettingsData
                {
                    Id = settings.Id,
                    Email = settings.Email,
                    Username = settings.Username,
                    CurrentPassword = settings.CurrentPassword,
                    Password1 = settings.Password1,
                    Password2 = settings.Password2
                 };

                var userSettings = _session.UserChangeData(data);
                if (userSettings.Status)
                {
                    HttpCookie cookie = _session.GenCookie(data.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Settings");
                }
                else
                {
                    ModelState.AddModelError("", userSettings.StatusMsg);
                    return View();
                }
            }

            return View();
        }
    }
}