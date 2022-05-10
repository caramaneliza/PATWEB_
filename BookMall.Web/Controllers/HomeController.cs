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
using BookMall.BuisnessLogic;
using BookMall.BuisnessLogic.Interfaces;

namespace BookMall.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProduct _product;
        public HomeController()
        {
            var bl = new BuissnesLogic();
            _product = bl.GetProductBL();
        }
        // GET: Home
        public ActionResult Index()
        {

            SessionStatus();
            GetUsername();
            GetUserLevel();

            var books = _product.GetProductList(1);
            List<ProductData> bookData = new List<ProductData>();
            foreach (var book in books)
            {
                bookData.Add(new ProductData
                {
                    Id = book.Id,
                    Author = book.Author,
                    ImageUrl = book.ImageUrl,
                    Title = book.Title,
                    Price = book.Price
                });
            }
            return View(bookData);
        }
    }
}