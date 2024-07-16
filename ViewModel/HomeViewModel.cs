using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple.Models;

namespace Simple.ViewModel
{
    public class HomeViewModel
    {
        public int RtrID { get; set; }
        public string RtrRealName { get; set; }
        public string RtrAvatar { get; set; }
        public string RtrCity { get; set; }
        public string RtrAddress { get; set; }
        public double Stars { get; set; }
        public string UserName { get; set; }
        public bool InActive { get; set; }
        public Nullable<int> MinPrice { get; set; }
        public Nullable<int> MaxPrice { get; set; }


    }
}