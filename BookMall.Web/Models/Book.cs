using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMall.Web.Models
{
    public class Book
    {
        public string Author { get; set; }
        public string Genre { get; set; }
        public float Price { get; set; }
        public int Pages { get; set; }
        
    }
}