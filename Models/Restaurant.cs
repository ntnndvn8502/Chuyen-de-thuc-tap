using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Simple.Models
{
    public class Restaurant
    {
        [Key]
        public int RtrID { get; set; }
        [Required]
        public string RtrRealName { get; set; }
        
        public string RtrEmail { get; set; }
        public string RtrPhone { get; set; }
        public string RtrCity { get; set; }
        public string RtrAddress { get; set; }
        public string RtrAvatar { get; set; }
        public string RtrDesc { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public Nullable<int> MinPrice { get; set; }
        public Nullable<int> MaxPrice { get; set; }

    }
}