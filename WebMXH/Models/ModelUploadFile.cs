using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMXH.Models
{
    public class ModelUploadFile
    {
        [Key]
        public int UserId { get; set; }

        public string HinhAnh { get; set; }

    }
}