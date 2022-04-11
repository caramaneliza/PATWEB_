using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using BookMall.Domain.Entities.User;
using BookMall.BuisnessLogic.DBModel;
using BookMall.Web.Models;

namespace BookMall.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //UDbTable q = new UDbTable();
            //q.Username = "catalin";
            //q.Password = "0f5624ce496375ad39d41440e4d92a35"; //catalinhimself + bookmall2022
            //q.Email = "catalinhimself@gmail.com";
            //q.LasIp = "127.0.0.1";
            //q.LastLogin = DateTime.Now;
            //
            //using (var db = new UserContext())
            //{
            //    db.Users.Add(q);
            //    db.SaveChanges();
            //}
            //
            UserData u = new UserData();
            u.Username = "catalin";

            return View(u);
        }
    }
}