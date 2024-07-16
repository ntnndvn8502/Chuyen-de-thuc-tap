using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Simple.RestaurantModels
{
    public class Register
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RtrRealName { get; set; }
        [Required]
        public string RtrCity { get; set; }
        [Required]
        public string RtrAddress { get; set; }
        public int RtrID { get; set; }

    }
}