using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.Domain.Enums;

namespace BookMall.Domain.Entities.User
{
    public class UProfileData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime FirstLogin { get; set; }
        public URole Level { get; set; }
    }
}
