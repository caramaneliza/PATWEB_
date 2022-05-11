using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.BuisnessLogic.Core;
using BookMall.BuisnessLogic.Interfaces;
using BookMall.Helpers.Session;
using BookMall.Domain.Entities.Product;
using BookMall.Domain.Entities.User;

namespace BookMall.BuisnessLogic
{
    public class ProductBL: ProductApi, IProduct
    {
        public PostResponse CreateProduct(PDbTable product)
        {
            return CreateProductAction(product);
        }
        public PostResponse EditProduct(PDbTable product)
        {
            return EditProductAction(product);
        }
        public List<ProductData> GetProductList(int page)
        {
            return GetProductListAction(page);
        }
        public PDbTable GetSingleProduct(int id)
        {
            return GetSingleProductAction(id);
        }
        public PostResponse DeleteProductById(int id)
        {
            return DeleteProductByIdAction(id);
        }
        //public List<ProductData> GetProductList()
        //{
        //    return GetProductListByUser();
        //}

        //public ProductData GetSingleProduct(int id)
        //{
        //    return new ProductData();
        //}
    }
}
