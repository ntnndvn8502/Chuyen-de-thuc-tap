using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Simple.Models;

namespace Simple.Identity
{
    public class AppUser : IdentityUser
    {
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Nullable<int> RtrID { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        


    }
}