using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BookMall.Domain.Entities.User; 

namespace BookMall.BuisnessLogic.DBModel
{
    public class UserContext :  DbContext
    {
        public UserContext() :
                    base("name=BookMall") // connectionstring name define in your web.config
        {
        }

        public virtual DbSet<UDbTable> Users { get; set; }
        public virtual DbSet<SessionsDbTable> Sessions { get; set; }
        //public virtual DbSet<Product> Products { get; set; }
    }
}
