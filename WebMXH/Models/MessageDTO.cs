using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMXH.Models
{
    public class MessageDTO
    {
        public int IDMes { get; set; }
        public string Message { get; set; }
        public string Class { get; set; }

        public DateTime Time { get; set; }
    }
}