using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMXH.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string HinhAnh { get; set; }

        public bool IsOnline { get; set; }
    }
}