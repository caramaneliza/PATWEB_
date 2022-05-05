using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMall.BuisnessLogic;
using BookMall.BuisnessLogic.Interfaces;

namespace BookMall.Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProduct _product;
        public ProductController()
        {
            var _bl = new BuissnesLogic();
            _product = _bl.GetProductBL();
        }


        // GET: Product
        public ActionResult Index()
        {
            var products = _product.GetProductList();
            int id = 1;
            var singleProduct = _product.GetSingleProduct(id);

            return View();
        }
    }
}