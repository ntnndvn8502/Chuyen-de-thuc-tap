namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "MinPrice", c => c.Int());
            AddColumn("dbo.Restaurants", "MaxPrice", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "MaxPrice");
            DropColumn("dbo.Restaurants", "MinPrice");
        }
    }
}
