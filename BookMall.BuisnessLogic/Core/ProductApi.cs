using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.BuisnessLogic.DBModel;
using BookMall.Domain.Entities.Product;
using BookMall.Domain.Entities.User;

namespace BookMall.BuisnessLogic.Core
{
    public class ProductApi
    {
        internal PostResponse CreateProductAction(PDbTable product)
        {
            if (product.Title == null || product.Title.Length == 0)
            {
                return new PostResponse { Status = false, StatusMsg = "Add Book Title" };
            }

            if (product.Author == null || product.Author.Length == 0)
            {
                return new PostResponse { Status = false, StatusMsg = "Add Book Author" };
            }

            using (var db = new UserContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            return new PostResponse { Status = true };
        }
    }
}
