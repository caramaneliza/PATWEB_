using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.Domain.Entities.Product;

namespace BookMall.BuisnessLogic.Interfaces
{
    public interface IProduct
    {
        List<ProductData> GetProductList();
        ProductData GetSingleProduct(int id);
    }
}
