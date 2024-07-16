using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Simple.Models
{
    public class Customer
    {
        [Key]
        public string CustomerID { get; set; }
        public string CustomerUserName { get; set; }
        public string CustomerRealName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
    }
}