using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Simple.Models
{
    public class Image
    {
        [Key]
        public int ImageID { get; set; }
        public string ImageUrl { get; set; }
        // Thêm các thuộc tính khác của Image nếu cần thiết

        // Foreign key property
        public int RtrID { get; set; }

        // Navigation property to represent the user
        public virtual Restaurant Restaurant { get; set; }
    }
}