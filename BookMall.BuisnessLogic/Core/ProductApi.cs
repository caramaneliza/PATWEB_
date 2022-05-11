using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        internal PostResponse EditProductAction(PDbTable product)
        {
            using (var db = new UserContext())
            {
                var tableProduct = db.Products.FirstOrDefault(p => p.Id == product.Id);
                if (tableProduct == null)
                {
                    return new PostResponse { Status = false, StatusMsg = "no such item id" };
                }
                if (product.Title != null && product.Title != tableProduct.Title)
                {
                    tableProduct.Title = product.Title;
                }

                if (product.Author != null && product.Author != tableProduct.Author)
                {
                    tableProduct.Author = product.Author;
                }

                if (product.Genre != null && product.Genre != tableProduct.Genre)
                {
                    tableProduct.Genre = product.Genre;
                }

                if (product.Description != null && product.Description != tableProduct.Description)
                {
                    tableProduct.Description = product.Description;
                }

                if (product.ISBN != null && product.ISBN != tableProduct.ISBN)
                {
                    tableProduct.ISBN = product.ISBN;
                }

                if (product.Price != null && product.Price != tableProduct.Price)
                {
                    tableProduct.Price = product.Price;
                }

                if (product.PdfFile != null && product.PdfFile != tableProduct.PdfFile)
                {
                    tableProduct.PdfFile = product.PdfFile;
                    tableProduct.PdfUrl = product.PdfUrl;
                    tableProduct.ImageUrl = product.ImageUrl;
                    tableProduct.JpgFile = product.JpgFile;
                    tableProduct.Pages = product.Pages;
                }
                db.Entry(tableProduct).State = EntityState.Modified;
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

        internal PostResponse DeleteProductByIdAction(int id)
        {
            using (var db = new UserContext())
            {
                var result = db.Products.FirstOrDefault(p => p.Id == id);
                if (result != null)
                {
                    db.Products.Remove(result);
                    db.SaveChanges();
                    return new PostResponse { Status = true, StatusMsg = "Deleted row: " + Newtonsoft.Json.JsonConvert.SerializeObject(result) };
                } else
                {
                    return new PostResponse { Status = false, StatusMsg = "Couldn't delete, no such id" };
                }
            }

        }
    }
}
