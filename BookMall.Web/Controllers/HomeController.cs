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
using BookMall.Web.App_Start;

namespace BookMall.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
           
            SessionStatus();
            GetUsername();
            GetUserLevel();

            return View();
        }
    }
}