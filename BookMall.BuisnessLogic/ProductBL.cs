using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.BuisnessLogic.Core;
using BookMall.BuisnessLogic.Interfaces;
using BookMall.Helpers.Session;
using BookMall.Domain.Entities.Product;

namespace BookMall.BuisnessLogic
{
    public class ProductBL: UserApi, IProduct
    {
        public List<ProductData> GetProductList()
        {
            return GetProductListByUser();
        }
        
        public ProductData GetSingleProduct(int id)
        {
            return new ProductData();
        }
    }
}
