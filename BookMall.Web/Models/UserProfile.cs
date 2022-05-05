using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookMall.Domain.Enums;

namespace BookMall.Web.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfileUrl { get; set; }
    }
}