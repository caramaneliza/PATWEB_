using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.BuisnessLogic.Interfaces;

namespace BookMall.BuisnessLogic
{
    public class BuissnesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IProduct GetProductBL()
        {
            return new ProductBL();
        }
    }
}
