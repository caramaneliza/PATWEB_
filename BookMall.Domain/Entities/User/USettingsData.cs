using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMall.Domain.Entities.User
{
    public class USettingsData
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }
    }
}
