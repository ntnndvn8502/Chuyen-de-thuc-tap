namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Rate", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Comment", c => c.String());
            DropColumn("dbo.Restaurants", "RtrStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Restaurants", "RtrStatus", c => c.String());
            DropColumn("dbo.Orders", "Comment");
            DropColumn("dbo.Orders", "Rate");
            DropColumn("dbo.Restaurants", "Active");
        }
    }
}
