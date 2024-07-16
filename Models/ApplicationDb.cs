using System;
using System.Data.Entity;
using System.Linq;

namespace Simple.Models
{
    public class ApplicationDb : DbContext
    {
        
        public ApplicationDb()
            : base("name=ApplicationDb")
        {
        }

        
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Simple.Models.Image> Images { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}