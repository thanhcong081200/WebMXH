using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMXH.Models
{
    public class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string SoDienThoai { get; set; }


        public bool RememberMe { get; set; }
    }
}