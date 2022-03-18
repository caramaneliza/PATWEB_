using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.Domain.Entities.User;
using BookMall.Domain.Entities.Product;

namespace BookMall.BuisnessLogic.Core
{
    public class UserApi
    {
        internal ULoginResp UserLoginAction(ULoginData data)
        {

            // ADD CHECK User DATA > Username & password

            return new ULoginResp { Status = true };
        }

        internal List<ProductData> GetProductListByUser()
        {
            return new List<ProductData>();
        }

    }
}
