using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.Domain.Entities.Product;
using BookMall.Domain.Entities.User;

namespace BookMall.BuisnessLogic.Interfaces
{
    public interface IProduct
    {
        PostResponse CreateProduct(PDbTable product);
        List<ProductData> GetProductList(int page);

        //ProductData GetSingleProduct(int id);
    }
}
