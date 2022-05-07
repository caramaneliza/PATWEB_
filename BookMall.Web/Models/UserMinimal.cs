using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMall.Web.Models
{
    public class UserMinimal
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Privilege { get; set; }
    }
}