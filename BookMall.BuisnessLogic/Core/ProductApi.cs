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

        internal List<ProductData> GetProductListAction(int page)
        {
            int onPage = 20;
            List<ProductData> productData = new List<ProductData>();
            using (var db = new UserContext())
            {
                var result = db.Products
                    .OrderBy(f => f.Id)
                    .Skip((page - 1) * onPage)
                    .Take(onPage)
                    .ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    productData.Add(new ProductData
                    {
                        Id = result[i].Id,
                        ImageUrl = result[i].ImageUrl,
                        Title = result[i].Title,
                        Author = result[i].Author,
                        Price = (decimal)result[i].Price
                    });
                }
            }
            return productData;
        }

        internal PDbTable GetSingleProductAction(int id)
        {
            using (var db = new UserContext())
            {
                return db.Products.FirstOrDefault(p => p.Id == id);
            }
        }
    }
}
