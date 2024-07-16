using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Simple.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("name=ApplicationDb")
        {

        }

       

        public System.Data.Entity.DbSet<Simple.Models.Restaurant> Restaurants { get; set; }
    }
}